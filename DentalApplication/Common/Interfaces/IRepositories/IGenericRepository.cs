namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task SaveChangesAsync();
        IQueryable<T> Table();
    }

}
