namespace Core.Specification.UserTag;

public class UserTagWithFiltersForCountSpecification : BaseSpecification<Entities.UserTag>
{
    public UserTagWithFiltersForCountSpecification(UserTagParams userTagParams)
        : base(x => (string.IsNullOrEmpty(userTagParams.Search) || x.Description.ToLower().Contains(userTagParams.Search))
                    && (!userTagParams.UserProfileId.HasValue || x.UserProfileId == userTagParams.UserProfileId))
    {
        
    }
}