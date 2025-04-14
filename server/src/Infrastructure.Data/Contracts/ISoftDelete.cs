namespace Infrastructure.Data.Contracts;

public interface ISoftDelete
{
    public DateTime? DeletedAt { get; set; }
}