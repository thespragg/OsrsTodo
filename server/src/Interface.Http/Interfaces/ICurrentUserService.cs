namespace Interface.Http.Interfaces;

internal interface ICurrentUserService
{
    Guid? UserId { get; }
}
