namespace Core.Specification.UserTag;

public class UserTagWithSpecificationParams : BaseSpecification<Entities.UserTag>
{
    public UserTagWithSpecificationParams(UserTagParams userTagParams)
        : base(x => (string.IsNullOrEmpty(userTagParams.Search) || x.Description.ToLower().Contains(userTagParams.Search))
                    && (!userTagParams.UserProfileId.HasValue || x.UserProfileId == userTagParams.UserProfileId)
                    && (userTagParams.IsActive == x.Active))
    {
        ApplyPaging(userTagParams.PageSize * (userTagParams.PageNumber -1), userTagParams.PageSize);
        
        if (!string.IsNullOrEmpty(userTagParams.SortByAlphabetical))
        {
            switch (userTagParams.SortByAlphabetical)
            {
                case "alphaAsc":
                    AddOrderBy(p => p.Description);
                    break;
                case "alphaDesc":
                    AddOrderByDescending(p => p.Description);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
        }
    }
}