using NoSolo.Core.Entities.Base;
using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.UserContact;

public class UserContactWithFiltersForCountSpecification : BaseSpecification<ContactEntity<Entities.User.UserEntity>>
{
    public UserContactWithFiltersForCountSpecification(UserContactParams userContactParams)
        : base(x => (string.IsNullOrEmpty(userContactParams.Search) || x.Text.ToLower().Contains(userContactParams.Search))
                    && (string.IsNullOrEmpty(userContactParams.Search) || x.Type.ToLower().Contains(userContactParams.Search))
                    && (!userContactParams.UserGuid.HasValue || x.TEntityId == userContactParams.UserGuid)
        )
    {
        
    }
}