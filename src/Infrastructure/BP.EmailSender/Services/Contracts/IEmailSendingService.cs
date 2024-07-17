using BP.EmailSender.Models;

namespace BP.EmailSender.Services.Contracts
{
    public interface IEmailSendingService
    {
        Task SendMessageAsync(MessageModel messageModel);
    }
}
