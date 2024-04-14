using Motorcycle.Domain.BudgetAggregate;

namespace Motorcycle.Application.Budget.Models.Response
{
    public record BudgetResponse
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string? Component { get; set; }
        public string? Model { get; set; }
        public bool Status { get; set; }

        public static explicit operator BudgetResponse(BudgetDomain v)
        {
            return new BudgetResponse
            {
                Id = v.Id,
                Price = v.Price,
                Component = v.Component
            };
        }
    }
}
