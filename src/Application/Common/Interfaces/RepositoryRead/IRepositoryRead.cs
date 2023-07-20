using UserPermission.API.Application.Common.DTOs;

namespace UserPermission.API.Application.Common.Interfaces.RepositoryRead
{
    public interface IRepositoryRead
    {
        Task<List<PermissionDto>> GetAll();
        Task Insert(PermissionDto permission);
        Task Update(PermissionDto permission);
    }
}
