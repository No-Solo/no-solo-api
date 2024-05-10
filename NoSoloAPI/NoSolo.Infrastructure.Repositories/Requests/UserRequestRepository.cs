using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Requests;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Infrastructure.Data.DbContext;

namespace NoSolo.Infrastructure.Repositories.Requests;

public class UserRequestRepository(DataBaseContext dataBaseContext) : IUserRequestRepository
{
    public async Task<RequestEntity<UserEntity, OrganizationOfferEntity>> GetRequest(Guid requestGuid)
    {
        return await dataBaseContext.Set<RequestEntity<UserEntity, OrganizationOfferEntity>>()
            .FindAsync(requestGuid);
    }

    public async Task<IReadOnlyList<RequestEntity<UserEntity, OrganizationOfferEntity>>> GetByUser(Guid userGuid)
    {
        return await dataBaseContext.Set<RequestEntity<UserEntity, OrganizationOfferEntity>>()
            .Where(x => x.TEntityId == userGuid).AsNoTracking().IgnoreAutoIncludes().ToListAsync();

    }

    public async Task<IReadOnlyList<RequestEntity<UserEntity, OrganizationOfferEntity>>> GetByOrganizationOffer(Guid organizationOfferGuid)
    {
        return await dataBaseContext.Set<RequestEntity<UserEntity, OrganizationOfferEntity>>()
            .Where(x => x.UEntityId == organizationOfferGuid).AsNoTracking().IgnoreAutoIncludes().ToListAsync();

    }

    public async Task<RequestEntity<UserEntity, OrganizationOfferEntity>> Get(Guid userGuid, Guid organizationOfferGuid)
    {
        return await dataBaseContext.Set<RequestEntity<UserEntity, OrganizationOfferEntity>>()
            .Where(x => x.TEntityId == userGuid && x.UEntityId == organizationOfferGuid).IgnoreAutoIncludes().SingleAsync();
    }

    public void Add(RequestEntity<UserEntity, OrganizationOfferEntity> requestEntity)
    {
        dataBaseContext.Set<RequestEntity<UserEntity, OrganizationOfferEntity>>().Add(requestEntity);
    }

    public void Delete(RequestEntity<UserEntity, OrganizationOfferEntity> requestEntity)
    {
        dataBaseContext.Set<RequestEntity<UserEntity, OrganizationOfferEntity>>().Remove(requestEntity);
    }

    public void Save()
    {
        if (dataBaseContext.ChangeTracker.HasChanges())
            dataBaseContext.SaveChanges();
    }
}