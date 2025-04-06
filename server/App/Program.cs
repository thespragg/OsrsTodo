using Infrastructure.Data.Extensions;
using Interface.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDataInfrastructure(builder.Configuration.GetConnectionString("DefaultDatabase")!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddEndpoints();

app.UseHttpsRedirection();

app.Run();