using BP.EmailSender.Models;
using BP.EmailSender.Settings;
using System.Net.Mail;
using System.Net;
using BP.EmailSender.Services.Contracts;
using Microsoft.Extensions.Options;

namespace BP.EmailSender.Services
{
    internal class EmailSendingService : IEmailSendingService
    {
        private readonly EmailSettings _emailOptions;

        public EmailSendingService(IOptions<EmailSettings> emailOptions)
        {
            _emailOptions = emailOptions.Value ?? throw new ArgumentNullException(nameof(emailOptions));
        }

        public async Task SendMessageAsync(MessageModel messageModel)
        {
            ArgumentNullException.ThrowIfNull(messageModel, nameof(messageModel));

            var sender = new MailAddress(_emailOptions.SenderEmail, _emailOptions.SenderName);
            var receiver = new MailAddress(messageModel.ReceiverEmail);

            var message = new MailMessage(sender, receiver)
            {
                Subject = messageModel.Subject,
                Body = messageModel.Body,
                IsBodyHtml = true
            };

            var smtp = new SmtpClient(_emailOptions.Host, _emailOptions.Port)
            {
                Credentials = new NetworkCredential(_emailOptions.SenderEmail, _emailOptions.AppCode),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}
