
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Motorcycle.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public ContextDb Context { get; set; }

        public UnitOfWork(ContextDb context)
        {
            Context = context;
        }

        public async Task CommitAsync()
        {
            using (var transaction = await this.Context.Database.BeginTransactionAsync())
            {
                try
                {
                    await this.Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task CommitWithIdentityInsertAsync(string table)
        {
            using (var transaction = await this.Context.Database.BeginTransactionAsync())
            {
                try
                {
                    Context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {table} ON;");
                    await this.Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
