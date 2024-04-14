using Motorcycle.Application.Historic.Models.Response;
using Motorcycle.Domain.HistoricAggregate;

namespace Motorcycle.Application.Historic.Services
{
    public class MaintenanceService(IMaintenanceRepository maintenanceRepository) : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository = maintenanceRepository;

        public async Task<IEnumerable<MaintenanceResponse>> GetAsync(int id)
        {
            var maintenances = await _maintenanceRepository.GetAsync(x => x.IdOwner == id);

            var responseList = maintenances.Select(maintenance => (MaintenanceResponse)maintenance).ToList();

            return responseList;
        }

    }
}
