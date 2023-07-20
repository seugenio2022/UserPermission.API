using UserPermission.API.Domain.Events;

namespace UserPermission.API.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Employee Employee { get; private set; }

        public Guid PermissionTypeId { get; private set; }
        public PermissionType PermissionType { get; private set; }

        private Permission()
        {

        }

        public Permission(string name, string description, Guid employeeId, Guid permissionTypeId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            EmployeeId = employeeId;
            PermissionTypeId = permissionTypeId;
        }

        public void Modify(string name, string description, Guid employeeId, Guid permissionTypeId)
        {
            Name = name;
            Description = description;
            EmployeeId = employeeId;
            PermissionTypeId = permissionTypeId;
        }
    }
}
