using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Web.API.Controllers;

public class RecommendationController : BaseApiController
{
    private readonly IRecommendService _recommendService;

    public RecommendationController(IRecommendService recommendService)
    {
        _recommendService = recommendService;
    }

    [HttpGet("users")]
    public async Task<IReadOnlyList<User>> GetRecommendedUsersForOrganizationOfferByTags(List<string> tags)
    {
        return await _recommendService.RecommendUsersForOrganizationOfferByTags(tags);
    }
    
    [HttpGet("organization")]
    public async Task<IReadOnlyList<Organization>> GetRecommendedOrganizationsForUserOfferByTags(List<string> tags)
    {
        return await _recommendService.RecommendOrganizationsForUserOfferByTags(tags);
    }
}