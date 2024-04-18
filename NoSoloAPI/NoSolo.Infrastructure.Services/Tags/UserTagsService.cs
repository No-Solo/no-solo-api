using AutoMapper;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Tags;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Users.Tags;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Users.UserTag;

namespace NoSolo.Infrastructure.Services.Tags;

public class UserTagsService : IUserTagsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private UserEntity _userEntity;
    
    public UserTagsService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = userService;
        _userEntity = null!;
    }
    
    public async Task<UserTagDto> Add(NewUserTagDto userTagDto, string email)
    {
        if (UserTagIsExist(userTagDto.Tag))
            throw new BadRequestException("This userEntity tag is already added to your profile");
        
        _userEntity = await _userService.GetUser(email, UserInclude.Tags);
        
        var userTag = new UserTagEntity()
        {
            UserGuid = _userEntity.Id,
            Tag = userTagDto.Tag,
            Active = userTagDto.Active
        };
        
        _userEntity.Tags.Add(userTag);

        return _mapper.Map<UserTagDto>(userTag);
    }

    public async Task<UserTagDto> Update(UpdateUserTagDto userTagDto, string email)
    {
        _userEntity = await _userService.GetUser(email, UserInclude.Tags);
        
        var tag = await GetTag(userTagDto.Id, _userEntity.Id);

        _mapper.Map(userTagDto, tag);

        return _mapper.Map<UserTagDto>(tag);
    }

    public async Task Delete(Guid userTagGuid, string email)
    {
        _userEntity = await _userService.GetUser(email, UserInclude.Tags);
        
        var tag = await GetTag(userTagGuid, _userEntity.Id);

        _userEntity.Tags.Remove(tag);
    }

    public async Task<UserTagDto> Get(Guid userTagGuid)
    {
        return _mapper.Map<UserTagDto>(await _unitOfWork.Repository<UserTagEntity>().GetByGuidAsync(userTagGuid));
    }

    public async Task<Pagination<UserTagDto>> Get(UserTagParams userTagParams)
    {
        var spec = new UserTagWithSpecificationParams(userTagParams);
        var countSpec = new UserTagWithFiltersForCountSpecification(userTagParams);

        var totalItems = await _unitOfWork.Repository<UserTagEntity>().CountAsync(countSpec);

        var tags = await _unitOfWork.Repository<UserTagEntity>().ListAsync(spec);
        var data = _mapper
            .Map<IReadOnlyList<UserTagEntity>, IReadOnlyList<UserTagDto>>(tags);

        return new Pagination<UserTagDto>(userTagParams.PageNumber, userTagParams.PageSize, totalItems, data);
    }

    public async Task<UserTagDto> ChangeActiveTask(Guid userTagGuid, string email)
    {
        _userEntity = await _userService.GetUser(email, UserInclude.Tags);
        
        var tag = await GetTag(userTagGuid, _userEntity.Id);

        tag.Active = !tag.Active;

        return _mapper.Map<UserTagDto>(tag);
    }

    private bool UserTagIsExist(string tag)
    {
        return (_userEntity.Tags.FirstOrDefault(t => t.Tag.ToLower() == tag.ToLower()) is not null);
    }
    
    private async Task<UserTagEntity> GetTag(Guid userTagGuid, Guid userGuid)
    {
        var userTagParams = new UserTagParams()
        {
            UserGuid = userGuid,
            UserTagGuid = userTagGuid
        };

        var spec = new UserTagWithSpecificationParams(userTagParams);

        var tag = await _unitOfWork.Repository<UserTagEntity>().GetEntityWithSpec(spec);
        if (tag is null)
            throw new EntityNotFound("The tag is not found");

        return tag;
    }
}