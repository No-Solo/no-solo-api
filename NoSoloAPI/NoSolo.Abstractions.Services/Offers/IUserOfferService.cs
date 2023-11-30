using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Abstractions.Services.Offers;

public interface IUserOfferService
{
    Task<UserOfferDto> Add(NewUserOfferDto userOfferDto, string email);

    Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams);
    Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams, Guid guid);
    Task<UserOfferDto> GetUserOffer(Guid offerGuid);

    Task<UserOfferDto> Update(UserOfferDto userOfferDto, string email);
    
    Task Delete(Guid offerGuid, string email);
}