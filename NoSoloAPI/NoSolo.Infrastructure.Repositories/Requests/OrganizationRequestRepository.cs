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

    public async Task<RequestEntity<OrganizationEntity, UserOfferEntity>> GetRequest(Guid requestGuid)
    {
        return await _dataBaseContext.Set<RequestEntity<OrganizationEntity, UserOfferEntity>>()
            .FindAsync(requestGuid);
    }

    public async Task<IReadOnlyList<RequestEntity<OrganizationEntity, UserOfferEntity>>> GetByOrganization(Guid organizationGuid)
    {
        return await _dataBaseContext.Set<RequestEntity<OrganizationEntity, UserOfferEntity>>()
            .Where(x => x.TEntityId == organizationGuid).AsNoTracking().IgnoreAutoIncludes().ToListAsync();
    }

    public async Task<IReadOnlyList<RequestEntity<OrganizationEntity, UserOfferEntity>>> GetByUserOffer(Guid userOfferGuid)
    {
        return await _dataBaseContext.Set<RequestEntity<OrganizationEntity, UserOfferEntity>>()
            .Where(x => x.UEntityId == userOfferGuid).AsNoTracking().IgnoreAutoIncludes().ToListAsync();
    }
    
    public async Task<RequestEntity<OrganizationEntity, UserOfferEntity>> Get(Guid organizationGuid, Guid userOfferGuid)
    {
        return await _dataBaseContext.Set<RequestEntity<OrganizationEntity, UserOfferEntity>>()
            .Where(x => x.TEntityId == organizationGuid && x.UEntityId == userOfferGuid).IgnoreAutoIncludes().SingleAsync();
    }

    public void Add(RequestEntity<OrganizationEntity, UserOfferEntity> requestEntity)
    {
        _dataBaseContext.Set<RequestEntity<OrganizationEntity, UserOfferEntity>>().Add(requestEntity);
    }

    public void Delete(RequestEntity<OrganizationEntity, UserOfferEntity> requestEntity)
    {
        _dataBaseContext.Set<RequestEntity<OrganizationEntity, UserOfferEntity>>().Remove(requestEntity);
    }

    public void Save()
    {
        if (_dataBaseContext.ChangeTracker.HasChanges())
            _dataBaseContext.SaveChanges();
    }
}