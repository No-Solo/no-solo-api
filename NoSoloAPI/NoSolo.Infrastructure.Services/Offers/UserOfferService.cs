using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Infrastructure.Services.Offers;

public class UserOfferService(IOfferService offerService, IUserService userService) : IUserOfferService
{
    private UserEntity? _user = null;

    public async Task<UserOfferDto> Add(NewUserOfferDto userOfferDto, string email)
    {
        _user ??= await userService.GetUser(email, UserInclude.Offers);

        return await offerService.Add(_user, userOfferDto);
    }


    public async Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams)
    {
        return await offerService.Get(userOfferParams);
    }

    public async Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams, Guid guid)
    {
        userOfferParams.UserGuid = guid;

        return await offerService.Get(userOfferParams);
    }

    public async Task<UserOfferDto> GetUserOffer(Guid offerGuid)
    {
        return await offerService.GetUserOfferDto(offerGuid);
    }

    public async Task<UserOfferDto> Update(UserOfferDto userOfferDto, string email)
    {
        _user ??= await userService.GetUser(email, UserInclude.Offers);

        return await offerService.Update(_user, userOfferDto);
    }

    public async Task Delete(Guid offerGuid, string email)
    {
        _user ??= await userService.GetUser(email, UserInclude.Offers);

        await offerService.Delete(_user, offerGuid);
    }
}