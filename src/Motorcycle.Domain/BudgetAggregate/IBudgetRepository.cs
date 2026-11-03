using Motorcycle.Domain.SeedWork;

namespace Motorcycle.Domain.BudgetAggregate
{
    public interface IBudgetRepository : IBaseRepository<BudgetDomain>
    {
        Task<List<BudgetDomain>> GetAllBudget(int id);
    }
}
