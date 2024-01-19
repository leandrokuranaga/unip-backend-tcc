namespace Motorcycle.Application.Motorcycle.Models.Request
{
    public class MotorcycleRequest
    {
        public int IdOwner { get; set; }
        public string? Model { get; set; }
        public bool Status { get; set; }
    }
}
