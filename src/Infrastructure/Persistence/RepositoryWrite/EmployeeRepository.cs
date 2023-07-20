using UserPermission.API.Domain.Entities;
using UserPermission.API.Domain.Interfaces;
using UserPermission.API.Infrastructure.Common;

namespace UserPermission.API.Infrastructure.Persistence.RepositoryWrite
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(UserPermissionDbContext context) : base(context)
        {
        }
    }
}
