using NoSolo.Abstractions.Base;
using NoSolo.Abstractions.Services;

namespace NoSolo.Abstractions.Repositories.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetByGuidAsync(Guid id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    void AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}