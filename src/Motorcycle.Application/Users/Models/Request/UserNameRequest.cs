using Motorcycle.Application.Users.Models.Response;

namespace Motorcycle.Application.Users.Models.Request
{
    public record UserNameRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator UserNameResponse(UserNameRequest v)
        {
            return new UserNameResponse
            {
                Name = v.Name
            };
        }
    }
}
