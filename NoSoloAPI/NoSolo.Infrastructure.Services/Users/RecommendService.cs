using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Infrastructure.Services.Users;

public class RecommendService : IRecommendService
{
    private readonly IUnitOfWork _unitOfWork;

    public RecommendService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<User>> RecommendUsersForOrganizationOfferByTags(List<string> tags)
    {
        var userProfiles = await _unitOfWork.Repository<User>().ListAllAsync();

        var targetUserProfiles = new List<User>();

        foreach (var userProfile in userProfiles)
        {
            var isExist = false;
            
            foreach (var tag in userProfile.Tags)
            {
                if (tag.Active)
                    if (tags.Contains(tag.Tag))
                        isExist = true;
            }
            
            if (isExist)
                targetUserProfiles.Add(userProfile);
        }

        return targetUserProfiles;
    }

    public async Task<IReadOnlyList<Organization>> RecommendOrganizationsForUserOfferByTags(List<string> tags)
    {
        var organizations = await _unitOfWork.Repository<Organization>().ListAllAsync();

        var targetOrganization = new List<Organization>();

        foreach (var organization in organizations)
        {
            var isExist = false;
            
            foreach (var offer in organization.Offers)
            {
                foreach (var tag in offer.Tags)
                {
                    if (tags.Contains(tag))
                        isExist = true;
                }
            }
            
            if (isExist)
                targetOrganization.Add(organization);
        }
        
        return targetOrganization;
    }

    // private IReadOnlyList<TSource> Get<TSource>(IReadOnlyList<TSource> items)
    // {
    //     
    // }
}