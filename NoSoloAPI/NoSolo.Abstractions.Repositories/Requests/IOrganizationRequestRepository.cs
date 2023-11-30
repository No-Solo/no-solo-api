using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Repositories.Requests;

public interface IOrganizationRequestRepository
{
    Task<Request<Organization, UserOffer>> GetRequest(Guid requestGuid);
    Task<IReadOnlyList<Request<Organization, UserOffer>>> GetByOrganization(Guid organizationGuid);
    Task<IReadOnlyList<Request<Organization, UserOffer>>> GetByUserOffer(Guid userOfferGuid);
    Task<Request<Organization, UserOffer>> Get(Guid organizationGuid, Guid userOfferGuid);
    void Add(Request<Organization, UserOffer> request);
    void Delete(Request<Organization, UserOffer> request);
    void Save();
}