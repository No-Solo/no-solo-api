﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Specification.Organization.OrganizationOffer;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Web.API.Controllers;

[ExcludeFromCodeCoverage]
[Route("api/recommendations")]
public class RecommendationController(IRecommendService recommendService) : BaseApiController
{
    [HttpGet("users")]
    public async Task<Pagination<UserOfferDto>> GetRecommendedUsersForOrganizationOfferByTags([FromQuery] UserOfferParams userOfferParams)
    {
        return await recommendService.RecommendUsersForOrganizationOfferByTags(userOfferParams);
    }
    
    [HttpGet("organizations")]
    public async Task<Pagination<OrganizationOfferDto>> GetRecommendedOrganizationsForUserOfferByTags([FromQuery] OrganizationOfferParams organizationOfferParams)
    {
        return await recommendService.RecommendOrganizationsForUserOfferByTags(organizationOfferParams);
    }
}