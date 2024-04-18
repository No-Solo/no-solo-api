using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.UserTag;

public class UserTagWithFiltersForCountSpecification : BaseSpecification<Entities.User.UserTagEntity>
{
    public UserTagWithFiltersForCountSpecification(UserTagParams userTagParams)
        : base(x => (string.IsNullOrEmpty(userTagParams.Search) || x.Tag.ToLower().Contains(userTagParams.Search))
                    && (!userTagParams.UserGuid.HasValue || x.UserGuid == userTagParams.UserGuid))
    {
        
    }
}