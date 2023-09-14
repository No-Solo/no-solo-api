using Core.Entities;

namespace Core.Specification;

public class UserProfileWithSpecificationParams : BaseSpecification<UserProfile>
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