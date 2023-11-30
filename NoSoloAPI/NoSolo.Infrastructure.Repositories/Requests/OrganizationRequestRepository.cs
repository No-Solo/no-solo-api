using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Requests;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Infrastructure.Data.DbContext;

namespace NoSolo.Infrastructure.Repositories.Requests;

public class OrganizationRequestRepository : IOrganizationRequestRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public OrganizationRequestRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<Request<Organization, UserOffer>> GetRequest(Guid requestGuid)
    {
        return await _dataBaseContext.Set<Request<Organization, UserOffer>>()
            .FindAsync(requestGuid);
    }

    public async Task<IReadOnlyList<Request<Organization, UserOffer>>> GetByOrganization(Guid organizationGuid)
    {
        return await _dataBaseContext.Set<Request<Organization, UserOffer>>()
            .Where(x => x.TEntityId == organizationGuid).AsNoTracking().IgnoreAutoIncludes().ToListAsync();
    }

    public async Task<IReadOnlyList<Request<Organization, UserOffer>>> GetByUserOffer(Guid userOfferGuid)
    {
        return await _dataBaseContext.Set<Request<Organization, UserOffer>>()
            .Where(x => x.UEntityId == userOfferGuid).AsNoTracking().IgnoreAutoIncludes().ToListAsync();
    }
    
    public async Task<Request<Organization, UserOffer>> Get(Guid organizationGuid, Guid userOfferGuid)
    {
        return await _dataBaseContext.Set<Request<Organization, UserOffer>>()
            .Where(x => x.TEntityId == organizationGuid && x.UEntityId == userOfferGuid).IgnoreAutoIncludes().SingleAsync();
    }

    public void Add(Request<Organization, UserOffer> request)
    {
        _dataBaseContext.Set<Request<Organization, UserOffer>>().Add(request);
    }

    public void Delete(Request<Organization, UserOffer> request)
    {
        _dataBaseContext.Set<Request<Organization, UserOffer>>().Remove(request);
    }

    public void Save()
    {
        if (_dataBaseContext.ChangeTracker.HasChanges())
            _dataBaseContext.SaveChanges();
    }
}