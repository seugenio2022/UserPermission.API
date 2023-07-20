namespace UserPermission.API.Application.Common.DTOs
{
    public record PermissionDto(Guid Id, string Name, string Description, Guid EmployeeId, Guid PermissionTypeId);
}
