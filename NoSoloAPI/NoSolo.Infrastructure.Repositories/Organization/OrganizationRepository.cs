using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Repositories;
using NoSolo.Infrastructure.Data.Data;

namespace NoSolo.Infrastructure.Repositories.Organization;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public OrganizationRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<Core.Entities.Organization.Organization> GetOrganizationWithoutIncludesByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Core.Entities.Organization.Organization> GetOrganizationWithAllIncludesByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Contacts)
            .Include(x => x.Project)
            .Include(x => x.Photos)
            .Include(x => x.Offers)
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Core.Entities.Organization.Organization> GetOrganizationWithOffersIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Offers)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Core.Entities.Organization.Organization> GetOrganizationWithMembersIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Core.Entities.Organization.Organization> GetOrganizationWithPhotosIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Core.Entities.Organization.Organization> GetOrganizationWithContactsIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Contacts)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Core.Entities.Organization.Organization> GetOrganizationWithProjectIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Project)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Core.Entities.Organization.Organization> GetOrganizationWithContactsAndMembersIncludesAsync(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Contacts)
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}