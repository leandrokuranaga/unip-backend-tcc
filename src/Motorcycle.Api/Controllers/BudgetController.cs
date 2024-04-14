using Microsoft.AspNetCore.Mvc;
using Motorcycle.Application.Budget.Models.Response;
using Motorcycle.Application.Budget.Services;

namespace motorcycle_tcc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController(IBudgetService budgetService) : Controller
    {
        private readonly IBudgetService _budgetService = budgetService;

        [HttpGet]
        public async Task<IEnumerable<BudgetResponse>> GetBudgetsync(int id)
        {
            return await _budgetService.GetBudgetAsync(id);
        }
    }
}
