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

    public async Task<IReadOnlyList<UserProfile>> RecommendUsersForOrganizationOfferByTags(List<TagEnum> tags)
    {
        var userProfiles = await _unitOfWork.Repository<UserProfile>().ListAllAsync();

        var targetUserProfiles = new List<UserProfile>();

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

    public async Task<IReadOnlyList<Organization>> RecommendOrganizationsForUserOfferByTags(List<TagEnum> tags)
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