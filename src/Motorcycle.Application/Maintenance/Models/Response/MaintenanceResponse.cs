using Motorcycle.Domain.MaintenanceAggregate;

namespace Motorcycle.Application.Historic.Models.Response
{
    public record MaintenanceResponse
    {
        public int Id { get; set; }
        public string? Service { get; set; }
        public DateOnly Date { get; set; }

        public static explicit operator MaintenanceResponse(MaintenanceDomain maintenance)
        {
            return new MaintenanceResponse
            {
                Id = maintenance.Id,
                Service = maintenance.Service,
                Date = maintenance.Date
            };
        }
    }
}
