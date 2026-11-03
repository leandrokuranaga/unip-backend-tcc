using Microsoft.AspNet.Identity.EntityFramework;

namespace Motorcycle.Application.Users.Models.Request
{
    public record UserRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public static explicit operator IdentityUser(UserRequest v)
        {
            return new IdentityUser
            {
                Email = v.Email,
                PasswordHash = v.Password
            };
        }
    }
}
