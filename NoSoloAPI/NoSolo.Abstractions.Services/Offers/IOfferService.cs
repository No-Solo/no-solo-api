using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Specification.Organization.OrganizationOffer;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Abstractions.Services.Offers;

public interface IOfferService
{
    Task<OrganizationOfferDto> Add(OrganizationEntity organizationEntity, NewOrganizationOfferDto organizationOfferDto);
    Task<UserOfferDto> Add(UserEntity userEntity, NewUserOfferDto userOfferDto);
    Task<Pagination<OrganizationOfferDto>> Get(OrganizationOfferParams organizationOfferParams);
    Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams);
    Task<OrganizationOfferDto> GetOrganizationOfferDto(Guid offerGuid);
    Task<UserOfferDto> GetUserOfferDto(Guid offerGuid);
    Task<OrganizationOfferEntity> Get(OrganizationEntity organizationEntity, Guid offerGuid);
    Task<UserOfferEntity> Get(UserEntity userEntity, Guid offerGuid);
    Task<OrganizationOfferDto> Update(OrganizationEntity organizationEntity, OrganizationOfferDto organizationOfferDto);
    Task<UserOfferDto> Update(UserEntity userEntity, UserOfferDto userOfferDto);
    Task Delete(OrganizationEntity organizationEntity, Guid offerGuid);
    Task Delete(UserEntity userEntity, Guid offerGuid);
}