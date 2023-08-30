using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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

    public void Delete(T entity)
    {
        _dataBaseContext.Set<T>().Remove(entity);
    }
}