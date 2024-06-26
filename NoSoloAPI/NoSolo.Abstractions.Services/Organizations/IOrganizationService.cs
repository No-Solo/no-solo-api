﻿using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Organizations;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;
using NoSolo.Core.Specification.Organization.Organization;

namespace NoSolo.Abstractions.Services.Organizations;

public interface IOrganizationService
{
    Task<OrganizationDto> Create(NewOrganizationDto organizationDto, string email);
    Task<OrganizationDto> AddMember(Guid organizationGuid, string email, string targetEmail);
    Task RemoveMember(Guid organizationGuid, string email, string targetEmail);
    Task UpdateRoleForMember(Guid organizationGuid, string email, string targetEmail, RoleEnum newRole);

    Task<Pagination<OrganizationDto>> GetMy(Guid memberGuid);
    Task<Pagination<OrganizationDto>> Get(OrganizationParams organizationParams);
    Task<OrganizationEntity> Get(Guid organizationGuid, OrganizationIncludeEnum include);
    Task<OrganizationEntity> Get(Guid organizationGuid, List<OrganizationIncludeEnum> includes);
    Task<OrganizationDto> Get(Guid organizationGuid);
    
    Task<OrganizationDto> Update(UpdateOrganizationDto organizationDto, string email);
    
    Task Delete(Guid organizationGuid, string email);
}