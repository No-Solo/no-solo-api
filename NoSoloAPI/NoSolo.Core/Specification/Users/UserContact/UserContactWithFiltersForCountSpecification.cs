using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Specification.BaseSpecification;
using NoSolo.Core.Specification.UserContact;

namespace NoSolo.Core.Specification.User.UserContact;

public class UserContactWithFiltersForCountSpecification : BaseSpecification<Contact<Entities.User.UserProfile>>
{
    public UserContactWithFiltersForCountSpecification(UserContactParams userContactParams)
        : base(x => (string.IsNullOrEmpty(userContactParams.Search) || x.Text.ToLower().Contains(userContactParams.Search)
                    && (string.IsNullOrEmpty(userContactParams.Search) || x.Type.ToLower().Contains(userContactParams.Search))
                    && (!userContactParams.UserProfileId.HasValue || x.TEntityId == userContactParams.UserProfileId)))
    {
        
    }
}