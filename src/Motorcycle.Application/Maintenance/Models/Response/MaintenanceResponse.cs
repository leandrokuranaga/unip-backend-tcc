namespace Motorcycle.Application.Historic.Models.Response
{
    public class MaintenanceResponse
    {
        public int Id { get; set; }
        public string? Service { get; set; }
        public DateOnly Date { get; set; }
    }
}
