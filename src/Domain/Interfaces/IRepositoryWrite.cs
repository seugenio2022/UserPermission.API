namespace UserPermission.API.Domain.Interfaces
{
    public interface IRepositoryWrite<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(Guid id);
    }
}
