using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Repositories.Requests;

public interface IUserRequestRepository
{
    Task<RequestEntity<UserEntity, OrganizationOfferEntity>> GetRequest(Guid requestGuid);
    Task<IReadOnlyList<RequestEntity<UserEntity, OrganizationOfferEntity>>> GetByUser(Guid userGuid);
    Task<IReadOnlyList<RequestEntity<UserEntity, OrganizationOfferEntity>>> GetByOrganizationOffer(Guid organizationOfferGuid);
    Task<RequestEntity<UserEntity, OrganizationOfferEntity>> Get(Guid userGuid, Guid organizationOfferGuid);
    void Add(RequestEntity<UserEntity, OrganizationOfferEntity> requestEntity);
    void Delete(RequestEntity<UserEntity, OrganizationOfferEntity> requestEntity);
    void Save();
}