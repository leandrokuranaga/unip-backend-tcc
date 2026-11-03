using Microsoft.Extensions.Configuration;
using Motorcycle.Infra.Data.ExternalServices;
using Motorcycle.Infra.Http.Email.Request;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;

namespace Motorcycle.Infra.ExternalServices
{
    public class EmailService : IEmailService
    {
        private const string APIKEY = "BREVO_API_KEY";
        private const string EMAILSENDER = "Sender_Email";
        private const string NAMESENDER = "Sender_Name";

        private const string SUBJECT = "Alteração de senha do sistema";
        
        private readonly string _apiKey;
        private readonly string _senderEmail;
        private readonly string _senderName;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration[APIKEY];
            _senderEmail = configuration[EMAILSENDER];
            _senderName = configuration[NAMESENDER];

            Configuration.Default.ApiKey["api-key"] = _apiKey;
        }

        public async System.Threading.Tasks.Task<bool> SendEmail(ContentEmail content)
        {

            var apiInstance = new TransactionalEmailsApi();
            SendSmtpEmailSender Email = new(_senderName, _senderEmail);
            SendSmtpEmailTo smtpEmailTo = new(content.Email, content.Name);
            List<SendSmtpEmailTo> To = new();
            To.Add(smtpEmailTo);
            
            string TextContent = $"Sua nova senha é {content.Password}";

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, null, TextContent, SUBJECT);
                return await System.Threading.Tasks.Task.Run(() =>
                {
                    CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                    var hasResponse = result.MessageId == string.Empty ? false : true;
                    return hasResponse;
                });
            }
            catch (Exception e)
            {
                throw;
            }

        }      
    }
}
