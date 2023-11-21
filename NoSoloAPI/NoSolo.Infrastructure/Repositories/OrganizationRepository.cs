using NoSolo.Core.Entities;
using NoSolo.Core.Entities.Organization;
using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Repositories;
using NoSolo.Infrastructure.Data;

namespace NoSolo.Infrastructure.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public OrganizationRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<Organization> GetOrganizationWithoutIncludesByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Organization> GetOrganizationWithAllIncludesByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Contacts)
            .Include(x => x.Project)
            .Include(x => x.Photos)
            .Include(x => x.Offers)
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Organization> GetOrganizationWithOffersIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Offers)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Organization> GetOrganizationWithMembersIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Organization> GetOrganizationWithPhotosIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Organization> GetOrganizationWithContactsIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Contacts)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Organization> GetOrganizationWithProjectIncludeByGuid(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Project)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Organization> GetOrganizationWithContactsAndMembersIncludesAsync(Guid id)
    {
        return await _dataBaseContext.Organizations
            .Include(x => x.Contacts)
            .Include(x => x.OrganizationUsers)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}