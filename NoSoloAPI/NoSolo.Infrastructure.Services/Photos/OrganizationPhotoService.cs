using AutoMapper;
using Microsoft.AspNetCore.Http;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organizations.Photos;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationPhotoParams;

namespace NoSolo.Infrastructure.Services.Photos;

public class OrganizationPhotoService : IOrganizationPhotoService
{
    private readonly IPhotoService _photoService;
    private readonly IOrganizationService _organizationService;
    private readonly IMemberService _memberService;

    public OrganizationPhotoService(IPhotoService photoService,
        IOrganizationService organizationService, IMemberService memberService)
    {
        _photoService = photoService;
        _organizationService = organizationService;
        _memberService = memberService;
    }

    public async Task<OrganizationPhotoDto> Add(IFormFile file, Guid organizationGuid, string email)
    {
        var organization = await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        return await _photoService.Add(organization, file);
    }

    public async Task<OrganizationPhotoDto> GetMain(Guid organizationGuid)
    {
        var organization = await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        return await _photoService.GetMainDto(organization);
    }

    public async Task<Pagination<OrganizationPhotoDto>> GetAll(Guid organizationGuid)
    {
        var organizationPhotoParams = new OrganizationPhotoParams() { OrganizationGuid = organizationGuid };

        return await _photoService.Get(organizationPhotoParams);
    }

    public async Task<OrganizationPhotoDto> SetMainPhoto(Guid photoGuid, Guid organizationGuid, string email)
    {
        var organization = await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Owner }, organizationGuid, email))
            throw new NotAccessException();

        return await _photoService.SetMainPhoto(organization, photoGuid);
    }

    public async Task Delete(Guid photoGuid, Guid organizationGuid, string email)
    {
        var organization = await _organizationService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator },
                organizationGuid, email))
            throw new NotAccessException();

        await _photoService.Delete(organization, photoGuid);
    }
}