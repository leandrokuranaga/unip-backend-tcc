
using Motorcycle.Infra.Http.Email.Request;

namespace Motorcycle.Infra.Data.ExternalServices
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(ContentEmail content);
    }
}
