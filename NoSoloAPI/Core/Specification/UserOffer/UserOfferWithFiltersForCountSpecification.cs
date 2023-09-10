namespace Core.Specification.UserOffer;

public class UserOfferWithFiltersForCountSpecification : BaseSpecification<Entities.UserOffer>
{
    public UserOfferWithFiltersForCountSpecification(UserOfferParams userOfferParams) 
        : base(x => (string.IsNullOrEmpty(userOfferParams.Search) || x.Preferences.ToLower().Contains(userOfferParams.Search))
        && (!userOfferParams.UserProfileId.HasValue || x.UserProfileId == userOfferParams.UserProfileId))
    {
        
    }
}