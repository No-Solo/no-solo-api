using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Repositories.Requests;

public interface IUserRequestRepository
{
    Task<Request<User, OrganizationOffer>> GetRequest(Guid requestGuid);
    Task<IReadOnlyList<Request<User, OrganizationOffer>>> GetByUser(Guid userGuid);
    Task<IReadOnlyList<Request<User, OrganizationOffer>>> GetByOrganizationOffer(Guid organizationOfferGuid);
    Task<Request<User, OrganizationOffer>> Get(Guid userGuid, Guid organizationOfferGuid);
    void Add(Request<User, OrganizationOffer> request);
    void Delete(Request<User, OrganizationOffer> request);
    void Save();
}