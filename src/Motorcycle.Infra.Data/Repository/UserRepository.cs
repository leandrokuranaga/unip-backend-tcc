using Motorcycle.Domain.UserAggregate;
using Motorcycle.Infra.Data;
using Motorcycle.Infra.Data.Repository;

namespace Motorcycle.Infra.Repository
{
    public class UserRepository : BaseRepository<ContextDb, UsersDomain>, IUserRepository
    {
        private readonly ContextDb _context;

        public UserRepository(ContextDb context) : base(context)
        {
            _context = context;
        }
    }
}
