
using Motorcycle.Application.Budget.Models.Response;

namespace Motorcycle.Application.Budget.Services
{
    public interface IBudgetService
    {
        Task<IEnumerable<BudgetResponse>> GetBudgetAsync(int id);
    }
}
