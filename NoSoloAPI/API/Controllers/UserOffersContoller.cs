using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Data;
using Core.Specification.UserOffer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "HasProfile")]
public class UserOffersContoller : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserOffersContoller(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpGet("offers", Name = "GetAllUserOffers")]
    public async Task<ActionResult<Pagination<UserOfferDto>>> GetAllOffers([FromQuery] UserOfferParams userOfferParams)
    {
        return Ok(await GetUserOffersBySpecificationParams(userOfferParams));
    }
    
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IReadOnlyList<UserOfferDto>>> GetOfferByGuid(Guid id)
    {
        return Ok(_mapper.Map<UserOfferDto>(await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(id)));
    }
    
    [HttpGet("my", Name = "GetMyUserOffers")]
    public async Task<ActionResult<Pagination<UserOfferDto>>> GetUserOffers([FromQuery] UserOfferParams userOfferParams)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());

        userOfferParams.UserProfileId = userProfile.Id;
        
        return Ok(await GetUserOffersBySpecificationParams(userOfferParams));
    }

    // [HttpGet("my/{id:guid}")]
    // public async Task<ActionResult<UserOfferDto>> GetUserOfferByGuid(Guid id)
    // {
    //     var userProfile =
    //         await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());
    //
    //     var userProfileWithOffer = await _unitOfWork.UserProfileRepository.GetUserProfileByOfferGuid(id);
    //
    //     if (userProfile.Id != userProfileWithOffer.Id)
    //         return NotFound(new ApiResponse(404, "The offer not found"));
    //     
    //     return Ok(_mapper.Map<UserOfferDto>(await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(id)));
    // }

    [HttpPost("add")]
    public async Task<ActionResult> AddUserOffer(CreateUserOfferDto userOfferDto)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());

        var userOffer = new UserOffer
        {
            Preferences = userOfferDto.Preferences
        };
        
        userProfile.Offers.Add(userOffer);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to create the offer"));
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<UserOfferDto>> UpdateUserOffer(UserOfferDto userOfferDto)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());
        
        if (await ComplianceCheck(userProfile.Id, userOfferDto.Id))
            return NotFound(new ApiResponse(404, "The offer not found"));

        var userOffer = await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(userOfferDto.Id);

        userOffer.Preferences = userOfferDto.Preferences;
        
        if (await _unitOfWork.Complete())
            return Ok(_mapper.Map<UserOfferDto>(userOffer));

        return BadRequest(new ApiResponse(400, "Failed to update the offer"));
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult> DeleteUserOffer(Guid id)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());

        if (await ComplianceCheck(userProfile.Id, id))
            return NotFound(new ApiResponse(404, "The offer not found"));

        var userOffer = await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(id);
        
        _unitOfWork.Repository<UserOffer>().Delete(userOffer);

        if (await _unitOfWork.Complete())
            return Ok();
        
        return BadRequest(new ApiResponse(400, "Failed to delete the offer"));
    }

    private async Task<bool> ComplianceCheck(Guid userProfileId, Guid offerId)
    {
        var userProfileWithOffer = await _unitOfWork.UserProfileRepository.GetUserProfileByOfferGuid(offerId);

        if (userProfileId != userProfileWithOffer.Id)
            return true;

        return false;
    }

    private async Task<Pagination<UserOfferDto>> GetUserOffersBySpecificationParams(UserOfferParams userOfferParams)
    {
        var spec = new UserOfferWithSpecificationParams(userOfferParams);
        
        var countSpec = new UserOfferWithFiltersForCountSpecification(userOfferParams);

        var totalItems = await _unitOfWork.Repository<UserOffer>().CountAsync(countSpec);
        
        var userOffers = await _unitOfWork.Repository<UserOffer>().ListAsync(spec);
        
        var data = _mapper
            .Map<IReadOnlyList<UserOffer>, IReadOnlyList<UserOfferDto>>(userOffers);

        return new Pagination<UserOfferDto>(userOfferParams.PageNumber, userOfferParams.PageSize, totalItems, data);
    }
}