using Motorcycle.Domain.MotorcycleAggregate;

namespace Motorcycle.Application.Motorcycle.Models.Response
{
    public record MotorcycleStatusResponse
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public bool Status { get; set; }

        public static explicit operator MotorcycleStatusResponse(MotorcycleDomain motor)
        {
            return new MotorcycleStatusResponse
            {
                Id = motor.Id,
                Model = motor.Model,
                Status = motor.Status,
            };
        }
    }
}
