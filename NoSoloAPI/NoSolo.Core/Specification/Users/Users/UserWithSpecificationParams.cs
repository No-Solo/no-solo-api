using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.Users;

public class UserWithSpecificationParams : BaseSpecification<Entities.User.User>
{
    public UserWithSpecificationParams(UserParams userParams) : base(x =>
        (string.IsNullOrEmpty(userParams.Email)) || x.Email.ToLower().Contains(userParams.Email))
    {
        if (userParams.UserProfileInclude)
        {
            AddInclude(x => x.UserProfile);
            AddInclude(x => x.UserProfile.Photo);
            AddInclude(x => x.UserProfile.Contacts);
            AddInclude(x => x.UserProfile.Offers);
            AddInclude(x => x.UserProfile.Tags);
            AddInclude(x => x.UserProfile.RequestsFromUserProfileToOgranizationOffer);
        }

        if (userParams.OrganizationsInclude)
            AddInclude(x => x.OrganizationUsers);
    }
}