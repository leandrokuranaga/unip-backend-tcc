using Motorcycle.Domain.MotorcycleAggregate;
using Motorcycle.Infra.Data;
using Motorcycle.Infra.Data.Repository;

namespace Motorcycle.Infra.Repository
{
    public class MotorcycleRepository : BaseRepository<ContextDb, MotorcycleDomain>, IMotorcycleRepository
    {
        private readonly ContextDb _context;

        public MotorcycleRepository(ContextDb context) : base(context)
        {
            _context = context;
        }
    }
}
