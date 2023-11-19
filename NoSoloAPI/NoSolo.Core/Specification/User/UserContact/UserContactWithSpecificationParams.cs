using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Specification.BaseSpecification;
using NoSolo.Core.Specification.UserContact;

namespace NoSolo.Core.Specification.User.UserContact;

public class UserContactWithSpecificationParams : BaseSpecification<Contact<Entities.User.UserProfile>>
{
    public UserContactWithSpecificationParams(UserContactParams userContactParams)
        : base(x => (string.IsNullOrEmpty(userContactParams.Search) || x.Text.ToLower().Contains(userContactParams.Search)
            && (string.IsNullOrEmpty(userContactParams.Search) || x.Type.ToLower().Contains(userContactParams.Search))
            && (!userContactParams.UserProfileId.HasValue || x.TEntityId == userContactParams.UserProfileId)))
    {
        ApplyPaging(userContactParams.PageSize * (userContactParams.PageNumber -1), userContactParams.PageSize);
        
        if (!string.IsNullOrEmpty(userContactParams.SortByAlphabetical))
        {
            switch (userContactParams.SortByAlphabetical)
            {
                case "alphaAsc":
                    AddOrderBy(p => p.Text);
                    break;
                case "alphaDesc":
                    AddOrderByDescending(p => p.Text);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
        }
    }
}