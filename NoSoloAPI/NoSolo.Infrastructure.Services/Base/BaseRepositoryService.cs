using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Services.Base;
using NoSolo.Abstractions.Services.Localization;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Exceptions;
using NoSolo.Infrastructure.Data.DbContext;
using NoSolo.Infrastructure.Services.Utility;

namespace NoSolo.Infrastructure.Services.Base;

public class BaseRepositoryService<T> : IBaseRepositoryService<T> where T : BaseCreatedEntity
{
    private readonly DataBaseContext _dataBaseContext;
    private readonly DbSet<T> _set;
    private readonly ILocalizationService _lang;

    public BaseRepositoryService(DataBaseContext dataBaseContext, DbSet<T> set, ILocalizationService localizationService)
    {
        _dataBaseContext = dataBaseContext;
        _set = set;
        _lang = localizationService;
    }
    
    virtual public Func<Guid, Task<bool>> OnBeforeGet { get; set; }
    virtual public Func<T, Task<bool>> OnAfterGet { get; set; }

    virtual public Func<List<T>, Task<bool>> OnAfterGetAll { get; set; }
    virtual public Func<Task<bool>> OnBeforeGetAll { get; set; }

    virtual public Func<T, Task<bool>> OnBeforePost { get; set; }
    virtual public Func<T, Task<bool>> OnAfterPost { get; set; }

    virtual public Func<T, Task<bool>> OnBeforePut { get; set; }
    virtual public Func<T, Task<bool>> OnAfterPut { get; set; }

    virtual public Func<Guid, Task<bool>> OnBeforeDelete { get; set; }
    virtual public Func<Guid, Task<bool>> OnAfterDelete { get; set; }
    
    virtual public async Task<List<T>> Get()
        {
            if (OnBeforeGetAll != null)
                await OnBeforeGetAll();

            var result = _set.OrderByDescending(c => c.DateCreated).ToList();

            if (OnAfterGetAll != null)
                await OnAfterGetAll(result);

            return result;
        }

        virtual public async Task<T> Get(Guid id)
        {
            if (id.IsEmpty())
                throw new ValidationException(_lang.GetSystem(LocalizationReservedKeys.IdCannotBeEmpty), AuthApiErrors.API_BAD_REQUEST);
            if (OnBeforeGet != null)
                await OnBeforeGet(id);

            var item = await _set.FirstOrDefaultAsync(c => c.Id == id);

            if (item.IsEmpty())
                throw new ValidationException(_lang.GetSystem(LocalizationReservedKeys.RecordNotFound), AuthApiErrors.API_NOT_FOUND);

            if (OnAfterGet != null)
                await OnAfterGet(item);

            return item;
        }


        virtual public async Task<T> Put(Guid id, T item)
        {
            if (OnBeforePut != null)
                await OnBeforePut(item);

            _set.Entry(item).State = EntityState.Modified;

            try
            {
                await _dataBaseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            if (OnAfterPut != null)
                await OnAfterPut(item);

            return item;
        }


        virtual public async Task<T> Post(T item)
        {
            if (OnBeforePost != null)
                await OnBeforePost(item);

            item.Id = Guid.NewGuid();
            item.Deleted = false;
            item.DateCreated = DateTime.UtcNow;
            
            _set.Add(item);
            try
            {
                await _dataBaseContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Exists(item.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            if (OnAfterPost != null)
                await OnAfterPost(item);

            return item;
        }

        virtual public async Task<bool> Delete(Guid id)
        {
            if (OnBeforeDelete != null)
                await OnBeforeDelete(id);

            var item = _set.FirstOrDefault(c => c.Id == id);

            if (item.IsEmpty())
                throw new ValidationException(_lang.GetSystem(LocalizationReservedKeys.RecordNotFound), AuthApiErrors.API_NOT_FOUND);
            
            item.Deleted = true;
            item.DateDeleted = DateTime.UtcNow;
            _dataBaseContext.Entry(item).State = EntityState.Modified;

            _set.Update(item);

            bool result = _dataBaseContext.SaveChanges() > 0;

            if (OnAfterDelete != null)
                await OnAfterDelete(id);

            return result;
        }
    

        virtual protected bool Exists(Guid id)
        {
            return (_set?.Any(e => e.Id == id)).GetValueOrDefault();
        }
}