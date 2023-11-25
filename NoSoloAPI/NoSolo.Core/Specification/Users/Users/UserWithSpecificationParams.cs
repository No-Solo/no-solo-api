using NoSolo.Core.Enums;
using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.Users;

public class UserWithSpecificationParams : BaseSpecification<Entities.User.User>
{
    public UserWithSpecificationParams(UserParams userParams) : base(x =>
        (string.IsNullOrEmpty(userParams.Email)) || x.Email.ToLower().Contains(userParams.Email))
    {
        foreach (var include in userParams.Includes)
        {
            ParseInclude(include);
        }
    }

    private void ParseInclude(UserInclude include)
    {
        switch (include)
        {
            case UserInclude.Contacts:
                AddInclude(c => c.Contacts);
                break;
            case UserInclude.Membership:
                AddInclude(m => m.OrganizationUsers);
                break;
            case UserInclude.Offers:
                AddInclude(o => o.Offers);
                break;
            case UserInclude.Tags:
                AddInclude(t => t.Tags);
                break;
            case UserInclude.Photo:
                AddInclude(p => p.Photo);
                break;
            case UserInclude.Requests:
                AddInclude(r => r.RequestsFromUserProfileToOgranizationOffer);
                break;
        }
    }
}