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
        [DisplayName("Id")]
        [Required]
        public int Id { get; set; }
        public bool Admin { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<MotorcycleDomain> Motorcycles { get; set; }
        public virtual ICollection<MaintenanceDomain> Maintenance { get; set; }
        public virtual ICollection<BudgetDomain> Budget { get; set; }
        public UsersDomain(int id, bool admin, string? name, string? email, string? password)
        {
            Id = id;
            Admin = admin;
            Name = name;
            Email = email;
            Password = password;
        }

        public UsersDomain()
        {
        }
    }
}
