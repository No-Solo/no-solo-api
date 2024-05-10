using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Specification.Organization.OrganizationOffer;
using NoSolo.Core.Specification.Users.UserOffer;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/offers")]
[ExcludeFromCodeCoverage]
public class OffersController(IUserOfferService userOfferService, IOrganizationOfferService organizationOfferService)
    : BaseApiController
{
    #region UserEntity Offers

    [HttpGet("userEntity")]
    public async Task<ActionResult<Pagination<UserOfferDto>>> GetAllOffers([FromQuery] UserOfferParams userOfferParams)
    {
        return await userOfferService.Get(userOfferParams);
    }

    [HttpGet("{offerGuid:guid}")]
    public async Task<ActionResult<UserOfferDto>> GetOfferByGuid(Guid offerGuid)
    {
        return await userOfferService.GetUserOffer(offerGuid);
    }

    [Authorize]
    [HttpGet("userEntity/my")]
    public async Task<ActionResult<Pagination<UserOfferDto>>> GetUserOffers([FromQuery] UserOfferParams userOfferParams)
    {
        return await userOfferService.Get(userOfferParams, User.GetUserId());
    }

    [Authorize]
    [HttpPost("userEntity/my/add")]
    public async Task<UserOfferDto> AddUserOffer(NewUserOfferDto userOfferDto)
    {
        return await userOfferService.Add(userOfferDto, User.GetEmail());
    }

    [Authorize]
    [HttpPut("userEntity/my/update")]
    public async Task<UserOfferDto> UpdateUserOffer(UserOfferDto userOfferDto)
    {
        return await userOfferService.Update(userOfferDto, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("userEntity/my/delete/{offerId:guid}")]
    public async Task DeleteUserOffer(Guid offerId)
    {
        await userOfferService.Delete(offerId, User.GetEmail());
    }

    #endregion

    #region OrganizationEntity Offers

    [HttpGet("organizationEntity/{organizationId:guid}")]
    public async Task<ActionResult<Pagination<OrganizationOfferDto>>> GetOrganizationOffersWithParams(
        Guid organizationId,
        [FromQuery] OrganizationOfferParams organizationOfferParams)
    {
        return await organizationOfferService.Get(organizationOfferParams, organizationId);
    }

    [HttpGet("organizationEntity")]
    public async Task<ActionResult<Pagination<OrganizationOfferDto>>> GetOffersWithParams(
        [FromQuery] OrganizationOfferParams organizationOfferParams)
    {
        return await organizationOfferService.Get(organizationOfferParams);
    }

    [Authorize]
    [HttpPost("organizationEntity/{organizationId:guid}/add")]
    public async Task<OrganizationOfferDto> CreateOrganizationOffer(Guid organizationId,
        [FromBody] NewOrganizationOfferDto organizationOfferDto)
    {
        return await organizationOfferService.Add(organizationOfferDto, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpPut("organizationEntity/{organizationId:guid}/update")]
    public async Task<OrganizationOfferDto> UpdateOrganizationOffer(Guid organizationId,
        [FromBody] OrganizationOfferDto organizationOfferDto)
    {
        return await organizationOfferService.Update(organizationOfferDto, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("organizationEntity/{organizationId:guid}/delete/{offerId:guid}")]
    public async Task DeleteOffer(Guid organizationId, Guid offerId)
    {
        await organizationOfferService.Delete(offerId, organizationId, User.GetEmail());
    }

    #endregion
}