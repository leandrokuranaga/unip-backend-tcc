using Motorcycle.Domain.HistoricAggregate;
using Motorcycle.Domain.MaintenanceAggregate;
using Motorcycle.Domain.UserAggregate;
using Motorcycle.Infra.Data;
using Motorcycle.Infra.Data.Repository;

namespace Motorcycle.Infra.Repository
{
    public class MaintenanceRepository : BaseRepository<ContextDb, MaintenanceDomain>, IMaintenanceRepository
    {
        private readonly ContextDb _context;

        public MaintenanceRepository(ContextDb context) : base(context)
        {
            _context = context;
        }
    }
}
