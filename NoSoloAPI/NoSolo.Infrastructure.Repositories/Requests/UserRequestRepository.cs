using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Requests;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Infrastructure.Data.DbContext;

namespace NoSolo.Infrastructure.Repositories.Requests;

public class UserRequestRepository : IUserRequestRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public UserRequestRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    
    public async Task<Request<User, OrganizationOffer>> GetRequest(Guid requestGuid)
    {
        return await _dataBaseContext.Set<Request<User, OrganizationOffer>>()
            .FindAsync(requestGuid);
    }

    public async Task<IReadOnlyList<Request<User, OrganizationOffer>>> GetByUser(Guid userGuid)
    {
        return await _dataBaseContext.Set<Request<User, OrganizationOffer>>()
            .Where(x => x.TEntityId == userGuid).AsNoTracking().IgnoreAutoIncludes().ToListAsync();

    }

    public async Task<IReadOnlyList<Request<User, OrganizationOffer>>> GetByOrganizationOffer(Guid organizationOfferGuid)
    {
        return await _dataBaseContext.Set<Request<User, OrganizationOffer>>()
            .Where(x => x.UEntityId == organizationOfferGuid).AsNoTracking().IgnoreAutoIncludes().ToListAsync();

    }

    public async Task<Request<User, OrganizationOffer>> Get(Guid userGuid, Guid organizationOfferGuid)
    {
        return await _dataBaseContext.Set<Request<User, OrganizationOffer>>()
            .Where(x => x.TEntityId == userGuid && x.UEntityId == organizationOfferGuid).IgnoreAutoIncludes().SingleAsync();
    }

    public void Add(Request<User, OrganizationOffer> request)
    {
        _dataBaseContext.Set<Request<User, OrganizationOffer>>().Add(request);
    }

    public void Delete(Request<User, OrganizationOffer> request)
    {
        _dataBaseContext.Set<Request<User, OrganizationOffer>>().Remove(request);
    }

    public void Save()
    {
        if (_dataBaseContext.ChangeTracker.HasChanges())
            _dataBaseContext.SaveChanges();
    }
}