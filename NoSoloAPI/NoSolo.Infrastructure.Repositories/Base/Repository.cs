using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Core.Specification.BaseSpecification;
using NoSolo.Infrastructure.Data.DbContext;
using NoSolo.Infrastructure.Data.Specification;

namespace NoSolo.Infrastructure.Repositories.Base;

public class Repository<T>(DataBaseContext dataBaseContext) : IRepository<T>
    where T : class
{
    public async Task<T?> GetByGuidAsync(Guid id)
    {
        return await dataBaseContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await dataBaseContext.Set<T>().ToListAsync();
    }

    public async void AddAsync(T entity)
    {
        await dataBaseContext.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        dataBaseContext.Set<T>().Attach(entity);
        dataBaseContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
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
        return SpecificationEvaluator<T>.GetQuery(dataBaseContext.Set<T>().AsQueryable(), spec);
    }

    public void Delete(T entity)
    {
        dataBaseContext.Set<T>().Remove(entity);
    }

    public void Save()
    {
        if (dataBaseContext.ChangeTracker.HasChanges())
            dataBaseContext.SaveChanges();
    }
}