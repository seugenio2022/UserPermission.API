
namespace UserPermission.API.Domain.Events
{
    public record PermissionModifiedEvent(Guid Id, string Name, string Description, Guid EmployeeId, Guid PermissionTypeId) : BaseEvent;
}
