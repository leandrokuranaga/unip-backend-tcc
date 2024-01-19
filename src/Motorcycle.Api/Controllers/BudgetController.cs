using Microsoft.AspNetCore.Mvc;
using Motorcycle.Application.Budget.Models.Response;
using Motorcycle.Application.Budget.Services;

namespace motorcycle_tcc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<IEnumerable<BudgetResponse>> GetBudgetsync(int id)
        {
            return await _budgetService.GetBudgetAsync(id);
        }
    }
}
