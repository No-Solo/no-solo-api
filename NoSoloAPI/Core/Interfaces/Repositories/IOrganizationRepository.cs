using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IOrganizationRepository
{
    Task<Organization> GetOrganizationWithoutIncludesByGuid(Guid id);
    Task<Organization> GetOrganizationWithAllIncludesByGuid(Guid id);
    Task<Organization> GetOrganizationWithOffersIncludeByGuid(Guid id);
    Task<Organization> GetOrganizationWithMembersIncludeByGuid(Guid id);
    Task<Organization> GetOrganizationWithPhotosIncludeByGuid(Guid id);
    Task<Organization> GetOrganizationWithContactsIncludeByGuid(Guid id);
    Task<Organization> GetOrganizationWithProjectIncludeByGuid(Guid id);
}