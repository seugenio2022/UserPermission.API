using UserPermission.API.Application.Common.Interfaces.RepositoryWrite;
using UserPermission.API.Domain.Entities;
using UserPermission.API.Infrastructure.Common;

namespace UserPermission.API.Infrastructure.Persistence.RepositoryWrite
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(UserPermissionDbContext context) : base(context)
        {
        }
    }
}