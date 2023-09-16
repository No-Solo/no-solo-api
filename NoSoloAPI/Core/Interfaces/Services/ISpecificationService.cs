using Core.Specification.UserContact;
using Core.Specification.UserOffer;
using Core.Specification.UserTag;

namespace Core.Interfaces.Services;

public interface ISpecificationService
{
    Task<Pagination<UserTagDto>> GetAllTagsBySpecificationParams(UserTagParams userTagParams);
    Task<Pagination<ContactDto>> GetUserContactsBySpecificationParams(UserContactParams userContactParams);
    Task<Pagination<UserOfferDto>> GetUserOffersBySpecificationParams(UserOfferParams userOfferParams);
}