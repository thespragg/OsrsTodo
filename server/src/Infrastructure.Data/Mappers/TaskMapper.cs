using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.Entities;
using Infrastructure.Data.Contracts;
using Infrastructure.Data.Extensions;
using Infrastructure.Data.Schema.Models;

namespace Infrastructure.Data.Mappers;

internal sealed class TaskMapper : EntityMapperBase<TaskDataModel, TaskEntity>
{
    public override Mapper Create()
        => new(new MapperConfiguration(cfg =>
        {
            cfg
                .CreateMap<TaskDataModel, TaskEntity>()
                .MapMember(x => x.Id, x => x.Id)
                .MapMember(x => x.UserId, x => x.UserId)
                .MapMember(x => x.Name, x => x.Name)
                .MapMember(x => x.Description, x => x.Description)
                .MapMember(x => x.InstancesPerDay, x => x.InstancesPerDay)
                .ReverseMap();

            cfg.AddExpressionMapping();
        }));
}