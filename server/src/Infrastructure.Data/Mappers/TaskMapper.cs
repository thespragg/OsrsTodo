using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.Entities;
using Infrastructure.Data.Contracts;
using Infrastructure.Data.Extensions;
using Infrastructure.Data.Schema.Models;

namespace Infrastructure.Data.Mappers;

internal sealed class TaskMapper : EntityMapperBase<TaskDataModel, TaskEntity>
{
    public override void ApplyTo(
        IMapperConfigurationExpression cfg
    ) => cfg
        .AddExpressionMapping()
        .CreateMap<TaskDataModel, TaskEntity>()
        .MapMember(x => x.Id, x => x.Id)
        .MapMember(x => x.UserId, x => x.UserId)
        .MapMember(x => x.Name, x => x.Name)
        .MapMember(x => x.Description, x => x.Description)
        .MapMember(x => x.InstancesPerDay, x => x.InstancesPerDay);
}