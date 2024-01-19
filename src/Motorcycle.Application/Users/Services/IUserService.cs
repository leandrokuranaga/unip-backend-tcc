using Motorcycle.Application.Users.Models.Request;
using Motorcycle.Application.Users.Models.Response;

namespace Motorcycle.Application.Users.Services
{
    public interface IUserService
    {
        public Task<UserResponse> CreateUserAsync(UserRequest request);
        public Task<UserNameResponse> UpdateUser(UserNameRequest request);
        public Task<UserPasswordResponse> UpdatePassword(UserPasswordRequest request);
        public Task<bool> Login(UserAuthRequest request);
        public Task ForgottenPasswordAsync(string email);
    }
}
