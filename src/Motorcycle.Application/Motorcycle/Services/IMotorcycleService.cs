using Motorcycle.Application.Motorcycle.Models.Request;
using Motorcycle.Application.Motorcycle.Models.Response;

namespace Motorcycle.Application.Motorcycle.Services
{
    public interface IMotorcycleService
    {
        Task<MotorcycleResponse> PostAsync(MotorcycleRequest motorcycleRequest);
        Task<bool> DeleteAsync(int id);
        Task<MotorcycleStatusResponse> GetStatus(int id);
    }
}
