using NoSolo.Core.Specification.BaseSpecification;
using NoSolo.Core.Specification.UserTag;

namespace NoSolo.Core.Specification.User.UserTag;

public class UserTagWithFiltersForCountSpecification : BaseSpecification<Entities.User.UserTag>
{
    public UserTagWithFiltersForCountSpecification(UserTagParams userTagParams)
        : base(x => (string.IsNullOrEmpty(userTagParams.Search) || x.Description.ToLower().Contains(userTagParams.Search))
                    && (!userTagParams.UserProfileId.HasValue || x.UserProfileId == userTagParams.UserProfileId))
    {
        
    }
}