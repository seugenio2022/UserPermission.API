namespace UserPermission.API.Domain.Events
{
    public record PermissionRequiredEvent(Guid Id, string Name, string Description, Guid EmployeeId, Guid PermissionTypeId) : BaseEvent;
}
