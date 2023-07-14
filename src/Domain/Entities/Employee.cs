namespace UserPermission.API.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Email{ get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
