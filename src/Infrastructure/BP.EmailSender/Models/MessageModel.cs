namespace BP.EmailSender.Models
{
    public class MessageModel
    {
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
