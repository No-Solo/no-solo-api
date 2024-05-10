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

public class UserTagsService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
    : IUserTagsService
{
    private UserEntity _userEntity = null!;

    public async Task<UserTagDto> Add(NewUserTagDto userTagDto, string email)
    {
        if (UserTagIsExist(userTagDto.Tag))
            throw new BadRequestException("This userEntity tag is already added to your profile");
        
        _userEntity = await userService.GetUser(email, UserInclude.Tags);
        
        var userTag = new UserTagEntity()
        {
            UserGuid = _userEntity.Id,
            Tag = userTagDto.Tag,
            Active = userTagDto.Active
        };
        
        _userEntity.Tags.Add(userTag);

        return mapper.Map<UserTagDto>(userTag);
    }

    public async Task<UserTagDto> Update(UpdateUserTagDto userTagDto, string email)
    {
        _userEntity = await userService.GetUser(email, UserInclude.Tags);
        
        var tag = await GetTag(userTagDto.Id, _userEntity.Id);

        mapper.Map(userTagDto, tag);

        return mapper.Map<UserTagDto>(tag);
    }

    public async Task Delete(Guid userTagGuid, string email)
    {
        _userEntity = await userService.GetUser(email, UserInclude.Tags);
        
        var tag = await GetTag(userTagGuid, _userEntity.Id);

        _userEntity.Tags.Remove(tag);
    }

    public async Task<UserTagDto> Get(Guid userTagGuid)
    {
        return mapper.Map<UserTagDto>(await unitOfWork.Repository<UserTagEntity>().GetByGuidAsync(userTagGuid));
    }

    public async Task<Pagination<UserTagDto>> Get(UserTagParams userTagParams)
    {
        var spec = new UserTagWithSpecificationParams(userTagParams);
        var countSpec = new UserTagWithFiltersForCountSpecification(userTagParams);

        var totalItems = await unitOfWork.Repository<UserTagEntity>().CountAsync(countSpec);

        var tags = await unitOfWork.Repository<UserTagEntity>().ListAsync(spec);
        var data = mapper
            .Map<IReadOnlyList<UserTagEntity>, IReadOnlyList<UserTagDto>>(tags);

        return new Pagination<UserTagDto>(userTagParams.PageNumber, userTagParams.PageSize, totalItems, data);
    }

    public async Task<UserTagDto> ChangeActiveTask(Guid userTagGuid, string email)
    {
        _userEntity = await userService.GetUser(email, UserInclude.Tags);
        
        var tag = await GetTag(userTagGuid, _userEntity.Id);

        tag.Active = !tag.Active;

        return mapper.Map<UserTagDto>(tag);
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

        var tag = await unitOfWork.Repository<UserTagEntity>().GetEntityWithSpec(spec);
        if (tag is null)
            throw new EntityNotFound("The tag is not found");

        return tag;
    }
}