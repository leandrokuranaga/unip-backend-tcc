
using Motorcycle.Application.Budget.Models.Response;
using Motorcycle.Domain.BudgetAggregate;

namespace Motorcycle.Application.Budget.Services
{
    public class BudgetService(IBudgetRepository budgetRepository) : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository = budgetRepository;

        public async Task<IEnumerable<BudgetResponse>> GetBudgetAsync(int id)
        {
            var budgets = await _budgetRepository.GetAllBudget(id);

            var result = budgets.Select(b => (BudgetResponse)b).ToList();

            return result;
        }
    }

}
