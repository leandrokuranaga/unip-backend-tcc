using Microsoft.AspNetCore.Mvc;
using Motorcycle.Application.Motorcycle.Models.Request;
using Motorcycle.Application.Motorcycle.Services;

namespace motorcycle_tcc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcycleController : Controller
    {
        private readonly IMotorcycleService _motorcycleService;

        public MotorcycleController(IMotorcycleService motorcycleService)
        {
            _motorcycleService = motorcycleService;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync(MotorcycleRequest motorcycleRequest)
        {
            await _motorcycleService.PostAsync(motorcycleRequest);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _motorcycleService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("status/{id}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            var status = await _motorcycleService.GetStatus(id);
            return Ok(status);
        }
    }
}
