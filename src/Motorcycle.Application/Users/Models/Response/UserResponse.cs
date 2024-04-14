namespace Motorcycle.Application.Users.Models.Response
{
    public record UserResponse
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
