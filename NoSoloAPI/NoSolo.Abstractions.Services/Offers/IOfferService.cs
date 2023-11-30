using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.User;
using NoSolo.Contracts.Dtos.User.Create;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Specification.Organization.OrganizationOffer;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Abstractions.Services.Offers;

public interface IOfferService
{
    Task<OrganizationOfferDto> Add(Organization organization, NewOrganizationOfferDto organizationOfferDto);
    Task<UserOfferDto> Add(User user, NewUserOfferDto userOfferDto);
    Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams);
    Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams);
    Task<OrganizationOfferDto> GetOrganizationOfferDto(Guid offerGuid);
    Task<UserOfferDto> GetUserOfferDto(Guid offerGuid);
    Task<OrganizationOffer> Get(Organization organization, Guid offerGuid);
    Task<UserOffer> Get(User user, Guid offerGuid);
    Task<OrganizationOfferDto> Update(Organization organization, OrganizationOfferDto organizationOfferDto);
    Task<UserOfferDto> Update(User user, UserOfferDto userOfferDto);
    Task Delete(Organization organization, Guid offerGuid);
    Task Delete(User user, Guid offerGuid);
}