using Abp.Net.Mail;
using Motorcycle.Application.Users.Models.Request;
using Motorcycle.Application.Users.Models.Response;
using Motorcycle.Domain.UserAggregate;
using Motorcycle.Infra.Data.ExternalServices;
using Motorcycle.Infra.Http.Email.Request;

namespace Motorcycle.Application.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        private const string NEWPASSWORD = "novasenha123";

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

            UsersDomain users = new UsersDomain
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
            };

            await _userRepository.InsertOrUpdateAsync(users);

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
            return await _userRepository.ExistAsync(x => x.Email == request.Email);
        }
        #endregion

        public async Task<bool> Login(UserAuthRequest request)
        {
            return await VerifyExistingUserAsync(request);
        }

        private async Task<bool> VerifyExistingUserAsync(UserAuthRequest request)
        {
            return await _userRepository.ExistAsync(x => x.Email == request.Email);
        }
        public async Task<UserPasswordResponse> UpdatePassword(UserPasswordRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, false);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.Id} not found.");
            }

            user.Password = request.Password;

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

            user.Name = request.Name;

            await _userRepository.UpdateAsync(user);

            UserNameResponse userResp = new()
            {
                Name = user.Name,
                Id = request.Id,
            };

            return userResp;
        }

        public async Task ForgottenPasswordAsync(string email)
        {
            var user = await _userRepository.GetOneNoTracking(x => x.Email.ToLower() == email.ToLower());

            user.Password = NEWPASSWORD;

            await _userRepository.UpdateAsync(user);

            ContentEmail content = new()
            {
                Email = email,
                Password = NEWPASSWORD,
                Name = user.Name
            };

            var response = await _emailService.SendEmail(content);
        
            if(!response)
            {                
                throw new Exception("Falha ao enviar o email.");
            }

        }
    }
}
