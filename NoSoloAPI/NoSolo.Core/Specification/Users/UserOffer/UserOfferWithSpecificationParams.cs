using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.UserOffer;

public class UserOfferWithSpecificationParams : BaseSpecification<Entities.User.UserOfferEntity>
{
    public UserOfferWithSpecificationParams(UserOfferParams userOfferParams)
        : base(x => (string.IsNullOrEmpty(userOfferParams.Search) || x.Preferences.ToLower().Contains(userOfferParams.Search))
                    && (!userOfferParams.UserGuid.HasValue || x.UserGuid == userOfferParams.UserGuid))
    {
        ApplyPaging(userOfferParams.PageSize * (userOfferParams.PageNumber -1), userOfferParams.PageSize);
        
        if (!string.IsNullOrEmpty(userOfferParams.SortByAlphabetical))
        {
            switch (userOfferParams.SortByAlphabetical)
            {
                case "alphaAsc":
                    AddOrderBy(p => p.Preferences);
                    break;
                case "alphaDesc":
                    AddOrderByDescending(p => p.Preferences);
                    break;
                default:
                    AddOrderBy(p => p.Created);
                    break;
            }
        }

        if (!string.IsNullOrEmpty(userOfferParams.SortByDate))
        {
            switch (userOfferParams.SortByDate)
            {
                case "dateAsc":
                    AddOrderBy(p => p.Created);
                    break;
                case "dateDesc":
                    AddOrderByDescending(p => p.Created);
                    break;
                default:
                    AddOrderBy(p => p.Created);
                    break;
            }
        }
    }
}