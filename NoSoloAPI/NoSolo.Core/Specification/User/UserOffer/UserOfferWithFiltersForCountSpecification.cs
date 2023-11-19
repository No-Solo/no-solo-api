using NoSolo.Core.Specification.BaseSpecification;
using NoSolo.Core.Specification.UserOffer;

namespace NoSolo.Core.Specification.User.UserOffer;

public class UserOfferWithFiltersForCountSpecification : BaseSpecification<Entities.User.UserOffer>
{
    public UserOfferWithFiltersForCountSpecification(UserOfferParams userOfferParams) 
        : base(x => (string.IsNullOrEmpty(userOfferParams.Search) || x.Preferences.ToLower().Contains(userOfferParams.Search))
        && (!userOfferParams.UserProfileId.HasValue || x.UserProfileId == userOfferParams.UserProfileId))
    {
        
    }
}