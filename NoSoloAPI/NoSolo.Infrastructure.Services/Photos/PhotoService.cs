using AutoMapper;
using Microsoft.AspNetCore.Http;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organizations.Photos;
using NoSolo.Contracts.Dtos.Users.Photo;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationPhotoParams;

namespace NoSolo.Infrastructure.Services.Photos;

public class PhotoService : IPhotoService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<OrganizationPhoto> _organizationPhotoRepository;
    private readonly IGenericRepository<UserPhoto> _userPhotoRepository;

    public PhotoService(ICloudinaryService cloudinaryService, IMapper mapper,
        IGenericRepository<OrganizationPhoto> organizationPhotoRepository,
        IGenericRepository<UserPhoto> userPhotoRepository)
    {
        _cloudinaryService = cloudinaryService;
        _mapper = mapper;
        _organizationPhotoRepository = organizationPhotoRepository;
        _userPhotoRepository = userPhotoRepository;
    }

    public async Task<OrganizationPhotoDto> Add(Organization organization, IFormFile file)
    {
        var result = await _cloudinaryService.AddPhotoAsync(file);
        if (result.Error is not null)
            throw new PhotoException($"Failed to add photo to organization: {result.Error.Message}");

        var photo = new OrganizationPhoto()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId,
            OrganizationId = organization.Id,
            Organization = organization
        };

        if (organization.Photos.Count is 0)
            photo.IsMain = true;

        _organizationPhotoRepository.AddAsync(photo);
        _organizationPhotoRepository.Save();

        return _mapper.Map<OrganizationPhotoDto>(photo);
    }

    public async Task<UserPhotoDto> Add(User user, IFormFile file)
    {
        if (user.Photo is not null)
            throw new PhotoException("You already have a photo");

        var result = await _cloudinaryService.AddPhotoAsync(file);

        if (result.Error is not null)
            throw new BadRequestException(result.Error.Message);

        var photo = new UserPhoto()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId,
            User = user,
            UserGuid = user.Id
        };

        _userPhotoRepository.AddAsync(photo);
        _userPhotoRepository.Save();
        
        return _mapper.Map<UserPhotoDto>(photo);
    }

    public async Task<OrganizationPhotoDto> GetMainDto(Organization organization)
    {
        var photo = await GetMain(organization);

        return _mapper.Map<OrganizationPhotoDto>(photo);
    }

    public async Task<UserPhotoDto> GetMainDto(User user)
    {
        if (user.Photo is null)
            throw new PhotoException("You don't have a photo");

        return _mapper.Map<UserPhotoDto>(user.Photo);
    }

    public async Task<OrganizationPhoto> GetMain(Organization organization)
    {
        var photo = organization.Photos.SingleOrDefault(p => p.IsMain == true);

        if (photo is null)
            throw new PhotoException("Failed to get a main photo of organization");

        return photo;
    }

    public async Task<OrganizationPhoto> Get(Organization organization, Guid photoGuid)
    {
        var photo = organization.Photos.SingleOrDefault(p => p.Id == photoGuid);

        if (photo is null)
            throw new PhotoException("The photo is not found");

        return photo;
    }

    public async Task<Pagination<OrganizationPhotoDto>> Get(OrganizationPhotoParams organizationPhotoParams)
    {
        var spec = new OrganizationPhotoWithSpecificationParams(organizationPhotoParams);

        var countSpec = new OrganizationPhotoWithPaginationForCountSpecification(organizationPhotoParams);

        var totalItems = await _organizationPhotoRepository.CountAsync(countSpec);

        var photos = await _organizationPhotoRepository.ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<OrganizationPhoto>, IReadOnlyList<OrganizationPhotoDto>>(photos);

        return new Pagination<OrganizationPhotoDto>(organizationPhotoParams.PageNumber,
            organizationPhotoParams.PageSize,
            totalItems, data);
    }

    public async Task<OrganizationPhotoDto> SetMainPhoto(Organization organization, Guid photoGuid)
    {
        var photo = await Get(organization, photoGuid);
        if (photo.IsMain)
            throw new PhotoException("This photo is already main");

        var currentMainPhoto = await GetMain(organization);

        currentMainPhoto.IsMain = false;
        photo.IsMain = true;

        _organizationPhotoRepository.Save();
        
        return _mapper.Map<OrganizationPhotoDto>(photo);
    }

    public async Task Delete(Organization organization, Guid photoGuid)
    {
        var photo = await Get(organization, photoGuid);
        if (photo.IsMain)
            throw new PhotoException("Failed to delete a main photo");

        if (photo.PublicId is not null)
        {
            var result = await _cloudinaryService.DeletePhotoAsync(photo.PublicId);

            if (result.Error is not null)
                throw new PhotoException($"Failed to delete a photo: {result.Error.Message}");

            _organizationPhotoRepository.Delete(photo);
            _organizationPhotoRepository.Save();
        }
    }

    public async Task Delete(User user)
    {
        if (user.Photo is null)
            throw new PhotoException("You don't have a photo");

        user.Photo = null;
        _userPhotoRepository.Save();
    }
}