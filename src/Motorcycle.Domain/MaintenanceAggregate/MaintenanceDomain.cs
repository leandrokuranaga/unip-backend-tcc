using Abp.Domain.Entities;
using Motorcycle.Domain.MotorcycleAggregate;
using Motorcycle.Domain.UserAggregate;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Motorcycle.Domain.MaintenanceAggregate
{
    public class MaintenanceDomain : Entity
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }
        public int IdMoto { get; set; }
        public int IdOwner { get; set; }
        public string? Service { get; set; }
        public DateOnly Date { get; set; }
        public virtual MotorcycleDomain Motorcycle { get; set; }
        public virtual UsersDomain User { get; set; }

        public MaintenanceDomain(int id, int idMoto, int idOwner, string? service, DateOnly date)
        {
            Id = id;
            IdMoto = idMoto;
            IdOwner = idOwner;
            Service = service;
            Date = date;
        }
    }
}
