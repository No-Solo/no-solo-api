using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organization;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Core.Specification.Organization.OrganizationOffer;

namespace NoSolo.Abstractions.Services.Offers;

public interface IOrganizationOfferService
{
    Task<OrganizationOfferDto> Add(NewOrganizationOfferDto organizationOfferDto, Guid organizationGuid, string email);
    
    Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams);
    Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams, Guid organizationGuid);
    Task<OrganizationOfferDto> GetOrganizationOffer(Guid offerGuid);
    
    Task<OrganizationOfferDto> Update(OrganizationOfferDto organizationOfferDto, Guid organizationGuid, string email);
    
    Task Delete(Guid offerGuid, Guid organizationGuid, string email);
}