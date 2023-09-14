using Core.Entities;

namespace Core.Specification.UserContact;

public class UserContactWithFiltersForCountSpecification : BaseSpecification<Entities.Contact<UserProfile>>
{
    public UserContactWithFiltersForCountSpecification(UserContactParams userContactParams)
        : base(x => (string.IsNullOrEmpty(userContactParams.Search) || x.Text.ToLower().Contains(userContactParams.Search)
                    && (string.IsNullOrEmpty(userContactParams.Search) || x.Type.ToLower().Contains(userContactParams.Search))
                    && (!userContactParams.UserProfileId.HasValue || x.TEntityId == userContactParams.UserProfileId)))
    {
        
    }
}