namespace UserPermission.API.Domain.Exceptions
{
    public class PermissionTypeNotFoundException : BusinessException
    {
        public PermissionTypeNotFoundException(Guid id) : base($"Permission Type with ID {id} does not exist")
        {
            
        }
    }
}
