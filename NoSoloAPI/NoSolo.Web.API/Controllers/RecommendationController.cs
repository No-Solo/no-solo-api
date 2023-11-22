using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Web.API.Controllers;

public class RecommendationController : BaseApiController
{
    private readonly IRecommendService _recommendService;

    public RecommendationController(IRecommendService recommendService)
    {
        _recommendService = recommendService;
    }

    [HttpGet("users")]
    public async Task<IReadOnlyList<UserProfile>> GetRecommendedUsersForOrganizationOfferByTags(List<TagEnum> tags)
    {
        return await _recommendService.RecommendUsersForOrganizationOfferByTags(tags);
    }
    
    [HttpGet("organization")]
    public async Task<IReadOnlyList<Organization>> GetRecommendedOrganizationsForUserOfferByTags(List<TagEnum> tags)
    {
        return await _recommendService.RecommendOrganizationsForUserOfferByTags(tags);
    }
}