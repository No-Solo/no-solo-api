using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Repositories.Requests;

public interface IOrganizationRequestRepository
{
    Task<RequestEntity<OrganizationEntity, UserOfferEntity>> GetRequest(Guid requestGuid);
    Task<IReadOnlyList<RequestEntity<OrganizationEntity, UserOfferEntity>>> GetByOrganization(Guid organizationGuid);
    Task<IReadOnlyList<RequestEntity<OrganizationEntity, UserOfferEntity>>> GetByUserOffer(Guid userOfferGuid);
    Task<RequestEntity<OrganizationEntity, UserOfferEntity>> Get(Guid organizationGuid, Guid userOfferGuid);
    void Add(RequestEntity<OrganizationEntity, UserOfferEntity> requestEntity);
    void Delete(RequestEntity<OrganizationEntity, UserOfferEntity> requestEntity);
    void Save();
}