
using Motorcycle.Application.Historic.Models.Response;

namespace Motorcycle.Application.Historic.Services
{
    public interface IMaintenanceService
    {
        Task<IEnumerable<MaintenanceResponse>> GetAsync(int id);
    }
}
