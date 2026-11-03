using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Motorcycle.Application.Users.Models.Request;
using Motorcycle.Application.Users.Models.Response;
using Motorcycle.Domain.UserAggregate;
using Motorcycle.Infra.Data.ExternalServices;
using Motorcycle.Infra.Http.Authenticate;
using Motorcycle.Infra.Http.Email.Request;
using System.Security.Cryptography;
using System.Text;

namespace Motorcycle.Application.Users.Services
{
    public class UserService(
        IUserRepository userRepository,
        IEmailService emailService
            ) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IEmailService _emailService = emailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private readonly IAuthenticateService _authenticationService;

        private string _newPassword = Guid.NewGuid().ToString();

        #region CreateUser
        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            var userExists = await VerifyExistingUserAsync(request.Email);

            if (userExists)
            {
                throw new Exception("User already exists"); 
            }

            var users = (IdentityUser)request;

            await _userManager.CreateAsync(users);

            UserResponse response = new()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
            };

            return response;
        }

        private async Task<bool> VerifyExistingUserAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user != null;
        }
        #endregion

        public async Task<bool> Login(UserAuthRequest request)
        {
            return await VerifyExistingUserAsync(request.Email);
        }

        public async Task<UserPasswordResponse> UpdatePassword(UserPasswordRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, false) ?? throw new KeyNotFoundException($"User with ID {request.Id} not found.");
            await _userRepository.UpdateAsync(user);

            var response = (UserPasswordResponse)request;

            return response;
        }

        public async Task<UserNameResponse> UpdateUser(UserNameRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, false) ?? throw new KeyNotFoundException($"User with ID {request.Id} not found.");
            await _userRepository.UpdateAsync(user);

            var userResp = (UserNameResponse)request;

            return userResp;
        }

        public async Task ForgottenPasswordAsync(string email)
        {
            IdentityUser user = new();

            user = _userManager.Users.Where(x => x.Email == email).FirstOrDefault();

            user.PasswordHash = _newPassword;

            await _userManager.CreateAsync(user);

            ContentEmail content = new()
            {
                Email = email,
                Password = HashPassword(_newPassword),
                Name = user.UserName
            };

            var response = await _emailService.SendEmail(content);
        
            if(!response)
            {                
                throw new Exception("Falha ao enviar o email.");
            }

        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
