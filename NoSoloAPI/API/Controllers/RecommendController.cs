using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RecommendController : BaseApiController
{
    private readonly IRecommendService _recommendService;

    public RecommendController(IRecommendService recommendService)
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