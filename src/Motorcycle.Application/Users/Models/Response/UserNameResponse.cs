namespace Motorcycle.Application.Users.Models.Response
{
    public record UserNameResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
