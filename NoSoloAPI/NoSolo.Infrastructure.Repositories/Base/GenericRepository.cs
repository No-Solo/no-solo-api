using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Base;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services;
using NoSolo.Infrastructure.Data.Data;
using NoSolo.Infrastructure.Data.DbContext;

namespace NoSolo.Infrastructure.Repositories.Base;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DataBaseContext _dataBaseContext;

    public GenericRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dataBaseContext.Set<T>().FindAsync(id);
    }

    public async Task<T> GetByGuidAsync(Guid id)
    {
        return await _dataBaseContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dataBaseContext.Set<T>().ToListAsync();
    }

    public async void AddAsync(T entity)
    {
        await _dataBaseContext.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dataBaseContext.Set<T>().Attach(entity);
        _dataBaseContext.Entry(entity).State = EntityState.Modified;
    }
    
    public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_dataBaseContext.Set<T>().AsQueryable(), spec);
    }

    public void Delete(T entity)
    {
        _dataBaseContext.Set<T>().Remove(entity);
    }
}