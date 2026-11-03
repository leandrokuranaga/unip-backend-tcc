using Motorcycle.Domain.MotorcycleAggregate;

namespace Motorcycle.Application.Motorcycle.Models.Response
{
    public record MotorcycleResponse
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public bool Status { get; set; }

        public static explicit operator MotorcycleResponse(MotorcycleDomain motorcycle)
        {
            return new MotorcycleResponse
            {
                Model = motorcycle.Model,
                Status = motorcycle.Status
            };
        }
    }
}
