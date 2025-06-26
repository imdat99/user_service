namespace Account.Shared.Interfaces;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
