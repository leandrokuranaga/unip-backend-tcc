using Motorcycle.Domain.BudgetAggregate;
using Motorcycle.Domain.MaintenanceAggregate;
using Motorcycle.Domain.UserAggregate;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Motorcycle.Domain.MotorcycleAggregate
{
    public class MotorcycleDomain
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }
        public int IdOwner { get; set; }
        public string? Model { get; set; }
        public bool Status { get; set; }
        public virtual UsersDomain User { get; set; }
        public virtual ICollection<MaintenanceDomain> Maintenances { get; set; }
        public virtual ICollection<BudgetDomain> Budget { get; set; }
        public MotorcycleDomain(int id, int idOwner, string? model, bool status)
        {
            Id = id;
            IdOwner = idOwner;
            Model = model;
            Status = status;
        }

        public MotorcycleDomain()
        {
        }
    }
}
