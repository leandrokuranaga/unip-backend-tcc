using Motorcycle.Application.Motorcycle.Models.Request;
using Motorcycle.Application.Motorcycle.Models.Response;
using Motorcycle.Domain.MotorcycleAggregate;

namespace Motorcycle.Application.Motorcycle.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(id, false);
            await _motorcycleRepository.DeleteAsync(motorcycle);

            return true;
        }

        public async Task<MotorcycleStatusResponse> GetStatus(int id)
        {
            var status = await _motorcycleRepository.GetByIdAsync(id, false);


            MotorcycleStatusResponse response = new MotorcycleStatusResponse 
            {
                Id = status.Id,
                Status = status.Status,
                Model = status.Model,
            };

            return response;
        }

        public async Task<MotorcycleResponse> PostAsync(MotorcycleRequest motorcycleRequest)
        {

            MotorcycleDomain motor = new()
            {
                IdOwner = motorcycleRequest.IdOwner,
                Model = motorcycleRequest.Model,
                Status = motorcycleRequest.Status,
            };
            
            await _motorcycleRepository.InsertOrUpdateAsync(motor);

            MotorcycleResponse response = new MotorcycleResponse() 
            {
                Status = motorcycleRequest.Status,
                Model = motorcycleRequest.Model,
            };

            return response;
        }
    }
}
