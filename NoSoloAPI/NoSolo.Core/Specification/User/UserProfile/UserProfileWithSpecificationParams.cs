using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.User.UserProfile;

public class UserProfileWithSpecificationParams : BaseSpecification<Entities.User.UserProfile>
{
    public UserProfileWithSpecificationParams(UserProfileParams userProfileParams)
        : base(x => (!userProfileParams.UserId.HasValue || x.UserId == userProfileParams.UserId))
    {
        AddOrderBy(x => x.Id);
        
        if (userProfileParams.WithContacts)
            AddInclude(x => x.Contacts);
        if (userProfileParams.WithOffers)
            AddInclude(x => x.Offers);
        if (userProfileParams.WithPhoto)
            AddInclude(x => x.Photo);
        if (userProfileParams.WithTags)
            AddInclude(x => x.Tags);
        
        ApplyPaging(userProfileParams.PageSize * (userProfileParams.PageNumber -1), userProfileParams.PageSize);
    }
}