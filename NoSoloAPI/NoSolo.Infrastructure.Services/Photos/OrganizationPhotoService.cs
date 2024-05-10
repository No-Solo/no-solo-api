using AutoMapper;
using Microsoft.AspNetCore.Http;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Photos;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationPhotoParams;

namespace NoSolo.Infrastructure.Services.Photos;

public class OrganizationPhotoService(
    IPhotoService photoService,
    IOrganizationService organizationService,
    IMemberService memberService)
    : IOrganizationPhotoService
{
    public async Task<OrganizationPhotoDto> Add(IFormFile file, Guid organizationGuid, string email)
    {
        var organization = await organizationService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        if (!await memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        return await photoService.Add(organization, file);
    }

    public async Task<OrganizationPhotoDto> GetMain(Guid organizationGuid)
    {
        var organization = await organizationService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        return await photoService.GetMainDto(organization);
    }

    public async Task<Pagination<OrganizationPhotoDto>> GetAll(Guid organizationGuid)
    {
        var organizationPhotoParams = new OrganizationPhotoParams() { OrganizationGuid = organizationGuid };

        return await photoService.Get(organizationPhotoParams);
    }

    public async Task<OrganizationPhotoDto> SetMainPhoto(Guid photoGuid, Guid organizationGuid, string email)
    {
        var organization = await organizationService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        if (!await memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Owner }, organizationGuid, email))
            throw new NotAccessException();

        return await photoService.SetMainPhoto(organization, photoGuid);
    }

    public async Task Delete(Guid photoGuid, Guid organizationGuid, string email)
    {
        var organization = await organizationService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        if (!await memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator },
                organizationGuid, email))
            throw new NotAccessException();

        await photoService.Delete(organization, photoGuid);
    }
}