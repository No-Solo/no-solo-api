using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.UserTag;

public class UserTagWithSpecificationParams : BaseSpecification<Entities.User.UserTagEntity>
{
    public UserTagWithSpecificationParams(UserTagParams userTagParams)
        : base(x => (string.IsNullOrEmpty(userTagParams.Search) || x.Tag.ToLower().Contains(userTagParams.Search))
                    && (!userTagParams.UserGuid.HasValue || x.UserGuid == userTagParams.UserGuid)
                    && (userTagParams.IsActive == x.Active))
    {
        if (!string.IsNullOrEmpty(userTagParams.SortByAlphabetical))
        {
            switch (userTagParams.SortByAlphabetical)
            {
                case "alphaAsc":
                    AddOrderBy(p => p.Tag);
                    break;
                case "alphaDesc":
                    AddOrderByDescending(p => p.Tag);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
        }
        
        ApplyPaging(userTagParams.PageSize * (userTagParams.PageNumber -1), userTagParams.PageSize);
    }
}