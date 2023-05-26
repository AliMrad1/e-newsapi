using System.Runtime.Serialization;

namespace BLC
{
    [Serializable]
    public class UserEmailExistException : Exception
    {
        public UserEmailExistException()
        {
        }

        public UserEmailExistException(string? message) : base(message)
        {
        }

        public UserEmailExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserEmailExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}