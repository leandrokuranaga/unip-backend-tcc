namespace Motorcycle.Infra.Data
{
    public interface IUnitOfWork
    {
        ContextDb Context { get; }
        Task CommitAsync();
        Task CommitWithIdentityInsertAsync(string table);
    }
}
