namespace UserPermission.API.Domain.Common
{
    public class BusinessException : Exception
    {
        public BusinessException(string msg) : base(msg)
        {
            
        }
    }
}
