namespace UserPermission.API.Application.Common.Interfaces.RepositoryWrite
{
    public interface IRepositoryWrite<T> where T : class
    {
        Task<T> Get(Guid id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(Guid id);
    }
}
