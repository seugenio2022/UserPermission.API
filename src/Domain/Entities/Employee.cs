using UserPermission.API.Domain.ValueObjects;

namespace UserPermission.API.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public Email Email{ get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
