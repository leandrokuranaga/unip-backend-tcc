using Microsoft.EntityFrameworkCore;
using Motorcycle.Application.Budget.Models.Response;
using Motorcycle.Domain.BudgetAggregate;
using Motorcycle.Infra.Data;
using Motorcycle.Infra.Data.Repository;

namespace Motorcycle.Infra.Repository
{
    public class BudgetRepository : BaseRepository<ContextDb, BudgetDomain>, IBudgetRepository
    {
        private readonly ContextDb _context;

        public BudgetRepository(ContextDb context) : base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<List<BudgetDomain>> GetAllBudget(int id)
        {
            var budgets = await _context.Budget
                .AsNoTracking()
                .Where(b => b.IdUsers == id)
                .OrderBy(b => b.Id)
                .ToListAsync();

            return budgets;
        }



    }
}
