using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Infrastructure.Services.Offers;

public class UserOfferService : IUserOfferService
{
    private readonly IOfferService _offerService;
    private readonly IUserService _userService;

    private UserEntity? _user;

    public UserOfferService(IOfferService offerService, IUserService userService)
    {
        _offerService = offerService;
        _userService = userService;

        _user = null;
    }

    public async Task<UserOfferDto> Add(NewUserOfferDto userOfferDto, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Offers);

        return await _offerService.Add(_user, userOfferDto);
    }


    public async Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams)
    {
        return await _offerService.Get(userOfferParams);
    }

    public async Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams, Guid guid)
    {
        userOfferParams.UserGuid = guid;

        return await _offerService.Get(userOfferParams);
    }

    public async Task<UserOfferDto> GetUserOffer(Guid offerGuid)
    {
        return await _offerService.GetUserOfferDto(offerGuid);
    }

    public async Task<UserOfferDto> Update(UserOfferDto userOfferDto, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Offers);

        return await _offerService.Update(_user, userOfferDto);
    }

    public async Task Delete(Guid offerGuid, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Offers);

        await _offerService.Delete(_user, offerGuid);
    }
}