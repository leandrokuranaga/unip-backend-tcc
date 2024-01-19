
using Motorcycle.Application.Budget.Models.Response;
using Motorcycle.Domain.BudgetAggregate;

namespace Motorcycle.Application.Budget.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<IEnumerable<BudgetResponse>> GetBudgetAsync(int id)
        {
            IEnumerable<BudgetResponse> response = new List<BudgetResponse>();

            var budgets = await _budgetRepository.GetAllBudget(id);

            var result = budgets.Select(b => new BudgetResponse
            {
                Id = b.Id,
                Price = b.Price,
                Component = b.Component
            }).ToList();

            return result;

        }
    }

}
