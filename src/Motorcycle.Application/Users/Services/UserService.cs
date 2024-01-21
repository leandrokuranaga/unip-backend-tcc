using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private readonly IAuthenticateService _authenticationService;

        private string _newPassword = Guid.NewGuid().ToString();

        public UserService(
            IUserRepository userRepository,
            IEmailService emailService
            )
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        #region CreateUser
        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            var userExists = await VerifyExistingUserAsync(request);

            if (userExists)
            {
                throw new Exception("User already exists"); 
            }

            IdentityUser users = new()
            {
                Email = request.Email,
                PasswordHash= request.Password,
            };

            await _userManager.CreateAsync(users);

            UserResponse response = new()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
            };

            return response;
        }

        private async Task<bool> VerifyExistingUserAsync(UserRequest request)
        {
            return await _userManager.Users.Where(x => x.Email == request.Email).FirstOrDefault();
        }
        #endregion

        public async Task<bool> Login(UserAuthRequest request)
        {
            return await VerifyExistingUserAsync(request);
        }

        private async Task<bool> VerifyExistingUserAsync(UserAuthRequest request)
        {
            return await _userManager.Users.Where(x => x.Email == request.Email).FirstOrDefault();
        }
        public async Task<UserPasswordResponse> UpdatePassword(UserPasswordRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, false);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.Id} not found.");
            }

            //user.Password = request.Password;

            await _userRepository.UpdateAsync(user);

            UserPasswordResponse response = new()
            {
                Password = request.Password,
                Id = request.Id
            };

            return response;
        }

        public async Task<UserNameResponse> UpdateUser(UserNameRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, false);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.Id} not found.");
            }

            //user.Name = request.Name;

            await _userRepository.UpdateAsync(user);

            UserNameResponse userResp = new()
            {
                //Name = user.Name,
                Id = request.Id,
            };

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
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
