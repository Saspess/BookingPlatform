namespace BP.Business.Common.Exceptions
{
    public class ServerConflictException : Exception
    {
        public ServerConflictException()
        {
        }

        public ServerConflictException(string message) : base(message)
        {
        }
    }
}
