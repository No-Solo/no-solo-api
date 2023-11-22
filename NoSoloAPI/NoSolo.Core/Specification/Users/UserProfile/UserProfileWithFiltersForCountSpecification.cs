using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.User.UserProfile;

public class UserProfileWithFiltersForCountSpecification : BaseSpecification<Entities.User.UserProfile>
{
    public UserProfileWithFiltersForCountSpecification(UserProfileParams userProfileParams)
        : base(x => (!userProfileParams.UserId.HasValue || x.UserId == userProfileParams.UserId))
    {
        
    }
}