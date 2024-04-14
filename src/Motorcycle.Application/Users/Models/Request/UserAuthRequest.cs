namespace Motorcycle.Application.Users.Models.Request
{
    public record UserAuthRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
