using System.Threading.RateLimiting;
using Domain.Extensions;
using Infrastructure.Data.Extensions;
using Interface.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddHttpInterface(builder.Configuration)
    .AddMemoryCache()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDataInfrastructure(builder.Configuration.GetConnectionString("DefaultDatabase")!)
    .AddDomain();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("fixed", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});

var app = builder
    .Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpInterface();
app.UseHttpsRedirection();

app.Run();