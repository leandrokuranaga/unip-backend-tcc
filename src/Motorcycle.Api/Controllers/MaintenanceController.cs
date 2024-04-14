using Microsoft.AspNetCore.Mvc;
using Motorcycle.Application.Historic.Models.Response;
using Motorcycle.Application.Historic.Services;

namespace motorcycle_tcc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController(IMaintenanceService historicService) : Controller
    {
        private readonly IMaintenanceService _maintenanceService = historicService;

        [HttpGet]
        public async Task<IEnumerable<MaintenanceResponse>> GetAsync(int id)
        {
            return await _maintenanceService.GetAsync(id);
        }
    }
}
