using Abp.Domain.Entities;
using Motorcycle.Domain.MotorcycleAggregate;
using Motorcycle.Domain.UserAggregate;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Motorcycle.Domain.BudgetAggregate
{
    public class BudgetDomain : Entity
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }
        public int IdUsers { get; set; }
        public int IdMotorcycle { get; set; }
        public double Price { get; set; }
        public string? Component { get; set; }

        public virtual UsersDomain User { get; set; }
        public virtual MotorcycleDomain Motorcycle { get; set; }

        public BudgetDomain(int id, int idUsers, int idMotorcycle, double price, string? component)
        {
            Id = id;
            IdUsers = idUsers;
            IdMotorcycle = idMotorcycle;
            Price = price;
            Component = component;
        }
    }
}
