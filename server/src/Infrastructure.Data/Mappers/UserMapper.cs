using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.Entities;
using Infrastructure.Data.Contracts;
using Infrastructure.Data.Extensions;
using Infrastructure.Data.Schema.Models;
using JetBrains.Annotations;

namespace Infrastructure.Data.Mappers;

[UsedImplicitly]
internal sealed class UserMapper : EntityMapperBase<UserDataModel, UserEntity>
{
    public override void ApplyTo(
        IMapperConfigurationExpression cfg
    )
    {
        cfg
            .AddExpressionMapping()
            .CreateMap<UserDataModel, UserEntity>()
            .MapMember(x => x.Id, x => x.Id)
            .MapMember(x => x.Username, x => x.Username)
            .MapMember(x => x.Email, x => x.Email)
            .MapMember(x => x.PasswordHash, x => x.PasswordHash)
            .MapMember(x => x.PasswordSalt, x => x.PasswordSalt)
            .MapMember(x => x.Role, x => x.Role)
            .MapMember(x => x.LastLoginAt, x => x.LastLoginAt)
            .MapMember(x => x.FailedLoginAttempts, x => x.FailedLoginAttempts)
            .MapMember(x => x.Accounts, x => x.Accounts)
            .ReverseMap();

        var accountMapper = new AccountMapper();
        accountMapper.ApplyTo(cfg);
    }
}