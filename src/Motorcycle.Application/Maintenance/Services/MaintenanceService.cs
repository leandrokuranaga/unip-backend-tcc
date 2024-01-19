using Motorcycle.Application.Historic.Models.Response;
using Motorcycle.Domain.HistoricAggregate;

namespace Motorcycle.Application.Historic.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        public MaintenanceService(IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }

        public async Task<IEnumerable<MaintenanceResponse>> GetAsync(int id)
        {
            var maintenances = await _maintenanceRepository.GetAsync(x => x.IdOwner == id);

            var responseList = new List<MaintenanceResponse>();
            foreach (var maintenance in maintenances)
            {
                var response = new MaintenanceResponse
                {
                    Id = maintenance.Id,
                    Service = maintenance.Service,
                    Date = maintenance.Date
                };

                responseList.Add(response);
            }

            return responseList;
        }

    }
}
