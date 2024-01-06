using NoSolo.Core.Entities.Base;

namespace NoSolo.Abstractions.Services.Base;

public interface IBaseRepositoryService<T> where T : BaseCreatedEntity
{
    public Func<Guid, Task<bool>> OnBeforeGet { get; set; }
    public Func<T, Task<bool>> OnAfterGet { get; set; }

    public Func<List<T>, Task<bool>> OnAfterGetAll { get; set; }
    public Func<Task<bool>> OnBeforeGetAll { get; set; }

    public Func<T, Task<bool>> OnBeforePost { get; set; }
    public Func<T, Task<bool>> OnAfterPost { get; set; }

    public Func<T, Task<bool>> OnBeforePut { get; set; }
    public Func<T, Task<bool>> OnAfterPut { get; set; }

    public Func<Guid, Task<bool>> OnBeforeDelete { get; set; }
    public Func<Guid, Task<bool>> OnAfterDelete { get; set; }

    public Task<List<T>> Get();
    public Task<T> Get(Guid id);

    public Task<T> Put(Guid id, T item);

    public Task<T> Post(T item);

    public Task<bool> Delete(Guid id);
}