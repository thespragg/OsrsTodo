using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.Entities;
using Infrastructure.Data.Contracts;
using Infrastructure.Data.Extensions;
using Infrastructure.Data.Schema.Models;

namespace Infrastructure.Data.Mappers;

internal sealed class TaskCompletionMapper : EntityMapperBase<TaskCompletionDataModel, TaskCompletionEntity>
{
    public override void ApplyTo(
        IMapperConfigurationExpression cfg
    ) => cfg
        .AddExpressionMapping()
        .CreateMap<TaskCompletionDataModel, TaskCompletionEntity>()
        .MapMember(x => x.Id, x => x.Id)
        .MapMember(x => x.UserId, x => x.UserId)
        .MapMember(x => x.TaskId, x => x.TaskId)
        .MapMember(x => x.TotalCompletions, x => x.TotalCompletions)
        .MapMember(x => x.CompletionDate, x => x.CompletionDate)
        .ReverseMap();
}