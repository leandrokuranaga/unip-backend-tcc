using Motorcycle.Domain.MotorcycleAggregate;

namespace Motorcycle.Application.Motorcycle.Models.Request
{
    public record MotorcycleRequest
    {
        public int IdOwner { get; set; }
        public string? Model { get; set; }
        public bool Status { get; set; }

        public static explicit operator MotorcycleDomain(MotorcycleRequest motorcycleRequest)
        {
            return new MotorcycleDomain
            {
                IdOwner = motorcycleRequest.IdOwner,
                Model = motorcycleRequest.Model,
                Status = motorcycleRequest.Status,
            };
        }
    }
}
