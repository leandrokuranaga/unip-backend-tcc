namespace Motorcycle.Application.Users.Models.Response
{
    public record UserPasswordResponse
    {
        public int Id { get; set; }
        public string? Password { get; set; }
    }
}
