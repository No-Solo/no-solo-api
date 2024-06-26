﻿using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Contacts;
using NoSolo.Core.Specification.Organization.OrganizationContact;

namespace NoSolo.Abstractions.Services.Contacts;

public interface IOrganizationContactService
{
    Task<ContactDto> Add(NewContactDto contactDto, Guid organizationGuid, string email);
    
    Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams);
    Task<ContactDto> Get(Guid contactGuid, Guid organizationGuid);

    Task<ContactDto> Update(ContactDto contactDto, Guid organizationGuid, string email);
    
    Task Delete(Guid contactGuid, Guid organizationGuid, string email);
}