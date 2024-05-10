using System.Collections;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Infrastructure.Data.DbContext;
using NoSolo.Infrastructure.Repositories.Base;

namespace NoSolo.Infrastructure.Repositories.UOW;

public class UnitOfWork(DataBaseContext dataBaseContext) : IUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly Hashtable _repositories = new Hashtable();

    public async ValueTask DisposeAsync()
    {
        await dataBaseContext.DisposeAsync();
    }

    public void Dispose()
    {
        _ = Task.Run(DisposeAsync, CancellationToken.None)
            .GetAwaiter()
            .GetResult();
    }
    

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), dataBaseContext);

            _repositories.Add(type, repositoryInstance);
        }

        return (IRepository<TEntity>)_repositories[type]!;
    }

    public async Task<bool> Complete()
    {
        return await dataBaseContext.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return dataBaseContext.ChangeTracker.HasChanges();
    }
}