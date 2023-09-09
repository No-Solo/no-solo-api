using Core.Entities;

namespace Core.Specification;

public class UserProfileWithFiltersForCountSpecification : BaseSpecification<UserProfile>
{
    public UserProfileWithFiltersForCountSpecification(UserProfileParams userProfileParams) 
        : base(x => (!userProfileParams.UserId.HasValue || x.UserId == userProfileParams.UserId))
    {
        
    }
}