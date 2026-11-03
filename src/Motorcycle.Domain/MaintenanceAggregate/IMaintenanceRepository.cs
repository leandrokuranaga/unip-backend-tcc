using Motorcycle.Domain.BudgetAggregate;
using Motorcycle.Domain.MaintenanceAggregate;
using Motorcycle.Domain.SeedWork;

namespace Motorcycle.Domain.HistoricAggregate
{
    public interface IMaintenanceRepository : IBaseRepository<MaintenanceDomain>
    {
    }
}
