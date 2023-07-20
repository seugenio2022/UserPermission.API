using UserPermission.API.Domain.Exceptions;
using UserPermission.API.Domain.Interfaces;

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

        public static async Task<Permission> CreateAsync(string name, string description, Guid employeeId, Guid permissionTypeId,
            IEmployeeRepository employeeRepository, IPermissionTypeRepository permissionTypeRepository)
        {
            await ThrowErrorOnInvalidRelations(employeeId, permissionTypeId, employeeRepository, permissionTypeRepository);

            return new Permission
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                EmployeeId = employeeId,
                PermissionTypeId = permissionTypeId
            };
        }


        public async Task ModifyAsync(string name, string description, Guid employeeId, Guid permissionTypeId,
            IEmployeeRepository employeeRepository, IPermissionTypeRepository permissionTypeRepository)
        {
            await ThrowErrorOnInvalidRelations(employeeId, permissionTypeId, employeeRepository, permissionTypeRepository);

            Name = name;
            Description = description;
            EmployeeId = employeeId;
            PermissionTypeId = permissionTypeId;
        }

        private static async Task ThrowErrorOnInvalidRelations(
            Guid employeeId,
            Guid permissionTypeId,
            IEmployeeRepository employeeRepository,
            IPermissionTypeRepository permissionTypeRepository)
        {
            var employee = await employeeRepository.GetAsync(employeeId);
            if (employee == null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }

            var permissionType = await permissionTypeRepository.GetAsync(permissionTypeId);
            if (permissionType == null)
            {
                throw new PermissionTypeNotFoundException(permissionTypeId);
            }
        }
    }
}
