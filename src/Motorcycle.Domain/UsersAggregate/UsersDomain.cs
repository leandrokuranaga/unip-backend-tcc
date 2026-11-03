using Abp.Domain.Entities;
using Motorcycle.Domain.BudgetAggregate;
using Motorcycle.Domain.MaintenanceAggregate;
using Motorcycle.Domain.MotorcycleAggregate;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Motorcycle.Domain.UserAggregate
{
    public class UsersDomain : Entity
    {
        public bool Admin { get; set; }
        public virtual ICollection<MotorcycleDomain> Motorcycles { get; set; }
        public virtual ICollection<MaintenanceDomain> Maintenance { get; set; }
        public virtual ICollection<BudgetDomain> Budget { get; set; }
    }
}
