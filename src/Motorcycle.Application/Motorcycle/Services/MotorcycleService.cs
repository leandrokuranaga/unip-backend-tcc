using Motorcycle.Application.Motorcycle.Models.Request;
using Motorcycle.Application.Motorcycle.Models.Response;
using Motorcycle.Domain.MotorcycleAggregate;

namespace Motorcycle.Application.Motorcycle.Services
{
    public class MotorcycleService(IMotorcycleRepository motorcycleRepository) : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;

        public async Task<bool> DeleteAsync(int id)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(id, false);
            await _motorcycleRepository.DeleteAsync(motorcycle);

            return true;
        }

        public async Task<MotorcycleStatusResponse> GetStatus(int id)
        {
            var status = await _motorcycleRepository.GetByIdAsync(id, false);

            var response = (MotorcycleStatusResponse)status;

            return response;
        }

        public async Task<MotorcycleResponse> PostAsync(MotorcycleRequest motorcycleRequest)
        {
            var motor = (MotorcycleDomain)motorcycleRequest;
            
            await _motorcycleRepository.InsertOrUpdateAsync(motor);

            var response = (MotorcycleResponse)motor;

            return response;
        }
    }
}
