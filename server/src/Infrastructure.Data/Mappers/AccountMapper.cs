using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.Entities;
using Infrastructure.Data.Contracts;
using Infrastructure.Data.Extensions;
using Infrastructure.Data.Schema.Models;

namespace Infrastructure.Data.Mappers;

internal sealed class AccountMapper : EntityMapperBase<AccountDataModel, AccountEntity>
{
    public override void ApplyTo(IMapperConfigurationExpression cfg)
        => cfg
            .AddExpressionMapping()
            .CreateMap<AccountDataModel, AccountEntity>()
            .MapMember(x => x.Id, x => x.Id)
            .MapMember(x => x.Username, x => x.Username)
            .MapMember(x => x.UserId, x => x.UserId)
            .ReverseMap();
}