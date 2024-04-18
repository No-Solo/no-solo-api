using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.UserOffer;

public class UserOfferWithFiltersForCountSpecification : BaseSpecification<Entities.User.UserOfferEntity>
{
    public UserOfferWithFiltersForCountSpecification(UserOfferParams userOfferParams) 
        : base(x => (string.IsNullOrEmpty(userOfferParams.Search) || x.Preferences.ToLower().Contains(userOfferParams.Search))
        && (!userOfferParams.UserGuid.HasValue || x.UserGuid == userOfferParams.UserGuid))
    {
        
    }
}