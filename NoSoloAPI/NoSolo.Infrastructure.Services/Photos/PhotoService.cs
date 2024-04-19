using AutoMapper;
using Microsoft.AspNetCore.Http;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
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
    private readonly IRepository<OrganizationPhotoEntity> _organizationPhotoRepository;
    private readonly IRepository<UserPhotoEntity> _userPhotoRepository;

    public PhotoService(ICloudinaryService cloudinaryService, IMapper mapper,
        IRepository<OrganizationPhotoEntity> organizationPhotoRepository,
        IRepository<UserPhotoEntity> userPhotoRepository)
    {
        _cloudinaryService = cloudinaryService;
        _mapper = mapper;
        _organizationPhotoRepository = organizationPhotoRepository;
        _userPhotoRepository = userPhotoRepository;
    }

    public async Task<OrganizationPhotoDto> Add(OrganizationEntity organizationEntity, IFormFile file)
    {
        var result = await _cloudinaryService.AddPhotoAsync(file);
        if (result.Error is not null)
            throw new PhotoException($"Failed to add photo to organizationEntity: {result.Error.Message}");

        var photo = new OrganizationPhotoEntity()
        {
            IsMain = false,
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId,
            OrganizationId = organizationEntity.Id,
            Organization = organizationEntity
        };

        if (organizationEntity.Photos.Count is 0)
            photo.IsMain = true;

        _organizationPhotoRepository.AddAsync(photo);
        _organizationPhotoRepository.Save();

        return _mapper.Map<OrganizationPhotoDto>(photo);
    }

    public async Task<UserPhotoDto> Add(UserEntity userEntity, IFormFile file)
    {
        if (userEntity.Photo is not null && userEntity.Photo.PublicId is not null)
        {
            await _cloudinaryService.DeletePhotoAsync(userEntity.Photo.PublicId);
            userEntity.Photo = null;
        }
        
        var result = await _cloudinaryService.AddPhotoAsync(file);

        if (result.Error is not null)
            throw new BadRequestException(result.Error.Message);

        var photo = new UserPhotoEntity()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId,
            User = userEntity,
            UserGuid = userEntity.Id
        };

        _userPhotoRepository.AddAsync(photo);
        _userPhotoRepository.Save();
        
        return _mapper.Map<UserPhotoDto>(photo);
    }

    public async Task<OrganizationPhotoDto> GetMainDto(OrganizationEntity organizationEntity)
    {
        var photo = await GetMain(organizationEntity);

        return _mapper.Map<OrganizationPhotoDto>(photo);
    }

    public Task<UserPhotoDto> GetMainDto(UserEntity userEntity)
    {
        if (userEntity.Photo is null)
            throw new PhotoException(404,"You don't have a photo");

        return Task.FromResult(_mapper.Map<UserPhotoDto>(userEntity.Photo));
    }

    public Task<OrganizationPhotoEntity> GetMain(OrganizationEntity organizationEntity)
    {
        var photo = organizationEntity.Photos.FirstOrDefault(p => p.IsMain == true);

        if (photo is null)
            throw new PhotoException("Failed to get a main photo of organizationEntity");

        return Task.FromResult(photo);
    }

    public Task<OrganizationPhotoEntity> Get(OrganizationEntity organizationEntity, Guid photoGuid)
    {
        var photo = organizationEntity.Photos.SingleOrDefault(p => p.Id == photoGuid);

        if (photo is null)
            throw new PhotoException("The photo is not found");

        return Task.FromResult(photo);
    }

    public async Task<Pagination<OrganizationPhotoDto>> Get(OrganizationPhotoParams organizationPhotoParams)
    {
        var spec = new OrganizationPhotoWithSpecificationParams(organizationPhotoParams);

        var countSpec = new OrganizationPhotoWithPaginationForCountSpecification(organizationPhotoParams);

        var totalItems = await _organizationPhotoRepository.CountAsync(countSpec);

        var photos = await _organizationPhotoRepository.ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<OrganizationPhotoEntity>, IReadOnlyList<OrganizationPhotoDto>>(photos);

        return new Pagination<OrganizationPhotoDto>(organizationPhotoParams.PageNumber,
            organizationPhotoParams.PageSize,
            totalItems, data);
    }

    public async Task<OrganizationPhotoDto> SetMainPhoto(OrganizationEntity organizationEntity, Guid photoGuid)
    {
        var photo = await Get(organizationEntity, photoGuid);
        if (photo.IsMain)
            throw new PhotoException("This photo is already main");

        var currentMainPhoto = await GetMain(organizationEntity);

        currentMainPhoto.IsMain = false;
        photo.IsMain = true;

        _organizationPhotoRepository.Save();
        
        return _mapper.Map<OrganizationPhotoDto>(photo);
    }

    public async Task Delete(OrganizationEntity organizationEntity, Guid photoGuid)
    {
        var photo = await Get(organizationEntity, photoGuid);
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

    public Task Delete(UserEntity userEntity)
    {
        if (userEntity.Photo is null)
            throw new PhotoException("You don't have a photo");

        userEntity.Photo = null;
        _userPhotoRepository.Save();
        
        return Task.CompletedTask;
    }
}