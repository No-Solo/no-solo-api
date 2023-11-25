using NoSolo.Core.Entities.Base;
using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.UserContact;

public class UserContactWithSpecificationParams : BaseSpecification<Contact<Entities.User.User>>
{
    public UserContactWithSpecificationParams(UserContactParams userContactParams)
        : base(x => (string.IsNullOrEmpty(userContactParams.Search) || x.Text.ToLower().Contains(userContactParams.Search)
            && (string.IsNullOrEmpty(userContactParams.Search) || x.Type.ToLower().Contains(userContactParams.Search))
            && (!userContactParams.UserGuid.HasValue || x.TEntityId == userContactParams.UserGuid)))
    {
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
        
        ApplyPaging(userContactParams.PageSize * (userContactParams.PageNumber -1), userContactParams.PageSize);
    }
}