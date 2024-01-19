namespace Motorcycle.Application.Budget.Models.Response
{
    public class BudgetResponse
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string? Component { get; set; }
        public string? Model { get; set; }
        public bool Status { get; set; }
    }
}
