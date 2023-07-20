namespace UserPermission.API.Application.Queries.GetPermissions
{
    public record GetPermissionsOutputDto(int Id, string Name, string Description, int EmployeeId, int PermissionTypeId);    
}
