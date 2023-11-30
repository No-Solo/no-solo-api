using System.Collections;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Infrastructure.Data.DbContext;
using NoSolo.Infrastructure.Repositories.Base;

namespace NoSolo.Infrastructure.Repositories.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataBaseContext _dataBaseContext;

    private Hashtable _repositories = new Hashtable();

    public UnitOfWork(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataBaseContext);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }

    public async Task<bool> Complete()
    {
        return await _dataBaseContext.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _dataBaseContext.ChangeTracker.HasChanges();
    }

    public void Dispose()
    {
        _dataBaseContext.Dispose();
    }
}