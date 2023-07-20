using UserPermission.API.Application.Common.Interfaces.RepositoryWrite;
using UserPermission.API.Infrastructure.Persistence;

namespace UserPermission.API.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; }

        public IPermissionRepository PermissionRepository { get; }

        public IPermissionTypeRepository PermissionTypeRepository { get; }

        private readonly UserPermissionDbContext _context;

        public UnitOfWork(UserPermissionDbContext context, IEmployeeRepository employeeRepository,
            IPermissionTypeRepository permissionTypeRepository, IPermissionRepository permissionRepository)
        {
            _context = context;
            PermissionTypeRepository = permissionTypeRepository;
            PermissionRepository = permissionRepository;
            EmployeeRepository = employeeRepository;
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
