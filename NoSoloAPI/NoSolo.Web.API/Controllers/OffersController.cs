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
public class OffersController : BaseApiController
{
    private readonly IUserOfferService _userOfferService;
    private readonly IOrganizationOfferService _organizationOfferService;

    public OffersController(IUserOfferService userOfferService, IOrganizationOfferService organizationOfferService)
    {
        _userOfferService = userOfferService;
        _organizationOfferService = organizationOfferService;
    }

    #region User Offers

    [HttpGet("user")]
    public async Task<ActionResult<Pagination<UserOfferDto>>> GetAllOffers([FromQuery] UserOfferParams userOfferParams)
    {
        return await _userOfferService.Get(userOfferParams);
    }

    [HttpGet("{offerGuid:guid}")]
    public async Task<ActionResult<UserOfferDto>> GetOfferByGuid(Guid offerGuid)
    {
        return await _userOfferService.GetUserOffer(offerGuid);
    }

    [Authorize]
    [HttpGet("user/my")]
    public async Task<ActionResult<Pagination<UserOfferDto>>> GetUserOffers([FromQuery] UserOfferParams userOfferParams)
    {
        return await _userOfferService.Get(userOfferParams, User.GetUserId());
    }

    [Authorize]
    [HttpPost("user/my/add")]
    public async Task<UserOfferDto> AddUserOffer(NewUserOfferDto userOfferDto)
    {
        return await _userOfferService.Add(userOfferDto, User.GetEmail());
    }

    [Authorize]
    [HttpPut("user/my/update")]
    public async Task<UserOfferDto> UpdateUserOffer(UserOfferDto userOfferDto)
    {
        return await _userOfferService.Update(userOfferDto, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("user/my/delete/{offerId:guid}")]
    public async Task DeleteUserOffer(Guid offerId)
    {
        await _userOfferService.Delete(offerId, User.GetEmail());
    }

    #endregion

    #region Organization Offers

    [HttpGet("organization/{organizationId:guid}")]
    public async Task<ActionResult<Pagination<OrganizationOfferDto>>> GetOrganizationOffersWithParams(
        Guid organizationId,
        [FromQuery] OrganizationOfferParams organizationOfferParams)
    {
        return await _organizationOfferService.Get(organizationOfferParams, organizationId);
    }

    [HttpGet("organization")]
    public async Task<ActionResult<Pagination<OrganizationOfferDto>>> GetOffersWithParams(
        [FromQuery] OrganizationOfferParams organizationOfferParams)
    {
        return await _organizationOfferService.Get(organizationOfferParams);
    }

    [Authorize]
    [HttpPost("organization/{organizationId:guid}/add")]
    public async Task<OrganizationOfferDto> CreateOrganizationOffer(Guid organizationId,
        [FromBody] NewOrganizationOfferDto organizationOfferDto)
    {
        return await _organizationOfferService.Add(organizationOfferDto, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpPut("organization/{organizationId:guid}/update")]
    public async Task<OrganizationOfferDto> UpdateOrganizationOffer(Guid organizationId,
        [FromBody] OrganizationOfferDto organizationOfferDto)
    {
        return await _organizationOfferService.Update(organizationOfferDto, organizationId, User.GetEmail());
    }

    [Authorize]
    [HttpDelete("organization/{organizationId:guid}/delete/{offerId:guid}")]
    public async Task DeleteOffer(Guid organizationId, Guid offerId)
    {
        await _organizationOfferService.Delete(offerId, organizationId, User.GetEmail());
    }

    #endregion
}