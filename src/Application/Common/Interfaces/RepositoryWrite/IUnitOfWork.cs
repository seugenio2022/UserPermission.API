namespace UserPermission.API.Application.Common.Interfaces.RepositoryWrite
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IPermissionTypeRepository PermissionTypeRepository { get; }
        Task<int> Save();
    }
}
