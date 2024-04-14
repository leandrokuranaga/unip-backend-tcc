using Motorcycle.Application.Users.Models.Response;

namespace Motorcycle.Application.Users.Models.Request
{
    public record UserPasswordRequest
    {
        public int Id { get; set; }
        public string? Password { get; set; }

        public static explicit operator UserPasswordResponse(UserPasswordRequest v)
        {
            return new UserPasswordResponse
            {
                Id = v.Id,
                Password = v.Password
            };
        }
    }
}
