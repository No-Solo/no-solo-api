using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Abstractions.Repositories.Base;

public interface IGenericRepository<T> where T : class
{
    // Task<T> GetByIdAsync(int id);
    Task<T> GetByGuidAsync(Guid id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);

    void AddAsync(T entity);

    // void Update(T entity);
    void Delete(T entity);
    void Save();
}