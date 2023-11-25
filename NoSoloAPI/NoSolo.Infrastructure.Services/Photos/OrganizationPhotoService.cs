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
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrganizaitonService _organizaitonService;
    private readonly IMapper _mapper;
    private readonly IMemberService _memberService;

    public OrganizationPhotoService(ICloudinaryService cloudinaryService, IUnitOfWork unitOfWork,
        IOrganizaitonService organizaitonService,
        IMapper mapper, IMemberService memberService)
    {
        _cloudinaryService = cloudinaryService;
        _unitOfWork = unitOfWork;
        _organizaitonService = organizaitonService;
        _mapper = mapper;
        _memberService = memberService;
    }

    public async Task<OrganizationPhotoDto> Add(IFormFile file, Guid organizationGuid, string email)
    {
        var organization = await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        var result = await _cloudinaryService.AddPhotoAsync(file);
        if (result.Error is not null)
            throw new PhotoException($"Failed to add photo to organization: {result.Error.Message}");

        var photo = new OrganizationPhoto()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        if (organization.Photos.Count is 0)
            photo.IsMain = true;

        organization.Photos.Add(photo);

        return _mapper.Map<OrganizationPhotoDto>(photo);
    }

    public async Task<OrganizationPhotoDto> GetMain(Guid organizationGuid)
    {
        var organization = await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        var photo = organization.Photos.SingleOrDefault(p => p.IsMain = true);

        if (photo is null)
            throw new PhotoException("Failed to get a main photo of organization");

        return _mapper.Map<OrganizationPhotoDto>(photo);
    }

    public async Task<Pagination<OrganizationPhotoDto>> GetAll(Guid organizationGuid)
    {
        var organizationPhotoParams = new OrganizationPhotoParams()
        {
            OrganizationGuid = organizationGuid
        };

        var spec = new OrganizationPhotoWithSpecificationParams(organizationPhotoParams);

        var countSpec = new OrganizationPhotoWithPaginationForCountSpecification(organizationPhotoParams);

        var totalItems = await _unitOfWork.Repository<OrganizationPhoto>().CountAsync(countSpec);

        var photos = await _unitOfWork.Repository<OrganizationPhoto>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<OrganizationPhoto>, IReadOnlyList<OrganizationPhotoDto>>(photos);

        return new Pagination<OrganizationPhotoDto>(organizationPhotoParams.PageNumber,
            organizationPhotoParams.PageSize,
            totalItems, data);
    }

    public async Task<OrganizationPhotoDto> SetMainPhoto(Guid photoGuid, Guid organizationGuid, string email)
    {
        var organization = await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Owner }, organizationGuid, email))
            throw new NotAccessException();

        var photo = organization.Photos.FirstOrDefault(p => p.Id == photoGuid);
        if (photo is null)
            throw new EntityNotFound("The photo is not found");
        if (photo.IsMain)
            throw new PhotoException("This photo is already main");

        var currentMainPhoto = organization.Photos.SingleOrDefault(p => p.IsMain == true);
        if (currentMainPhoto is not null)
        {
            currentMainPhoto.IsMain = false;
            photo.IsMain = true;
            return _mapper.Map<OrganizationPhotoDto>(photo);
        }

        return _mapper.Map<OrganizationPhotoDto>(currentMainPhoto);
    }

    public async Task Delete(Guid photoGuid, Guid organizationGuid, string email)
    {
        var organization = await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Photos);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator },
                organizationGuid, email))
            throw new NotAccessException();

        var photo = organization.Photos.FirstOrDefault(p => p.Id == photoGuid);
        if (photo is null)
            throw new EntityNotFound("The photo is not found");
        if (photo.IsMain)
            throw new PhotoException("Failed to delete a main photo");

        if (photo.PublicId is not null)
        {
            var result = await _cloudinaryService.DeletePhotoAsync(photo.PublicId);

            if (result.Error is not null)
                throw new PhotoException($"Failed to delete a photo: {result.Error.Message}");

            organization.Photos.Remove(photo);
        }
    }
}