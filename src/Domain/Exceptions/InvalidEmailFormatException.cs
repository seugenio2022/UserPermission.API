namespace UserPermission.API.Domain.Exceptions
{
    public class InvalidEmailFormatException : BusinessException
    {
        public InvalidEmailFormatException(string email)
        : base($"Invalid email address format: \"{email}\"")
        {
        }
    }
}
