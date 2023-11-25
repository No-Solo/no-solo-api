using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Base.Create;
using NoSolo.Core.Specification.OrganizationContact;

namespace NoSolo.Abstractions.Services.Contacts;

public interface IOrganizationContactService
{
    Task<ContactDto> Add(NewContactDto contactDto, Guid organizationGuid, string email);
    
    Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams, Guid organizationGuid);
    Task<ContactDto> Get(Guid contactGuid, Guid organizationGuid);

    Task<ContactDto> Update(ContactDto contactDto, Guid organizationGuid, string email);
    
    Task Delete(Guid contactGuid, Guid organizationGuid, string email);
}