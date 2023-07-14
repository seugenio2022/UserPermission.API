namespace UserPermission.API.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int PermissionTypeId { get; set; }
        public PermissionType PermissionType { get; set; }
    }
}
