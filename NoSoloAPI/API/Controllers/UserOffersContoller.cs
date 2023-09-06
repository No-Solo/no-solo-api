using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "RegisteredUser")]
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
    public async Task<ActionResult<IReadOnlyList<UserOfferDto>>> GetAllOffers()
    {
        return Ok(_mapper.Map<IReadOnlyList<UserOfferDto>>(await _unitOfWork.Repository<UserOffer>().ListAllAsync()));
    }
    
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IReadOnlyList<UserOfferDto>>> GetOfferByGuid(Guid id)
    {
        return Ok(_mapper.Map<UserOfferDto>(await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(id)));
    }
    
    [HttpGet("my", Name = "GetMyUserOffers")]
    public async Task<ActionResult<IReadOnlyList<UserOfferDto>>> GetUserOffers()
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());

        return Ok(_mapper.Map<UserOfferDto>(userProfile.Offers));
    }

    [HttpGet("my/{id:guid}")]
    public async Task<ActionResult<UserOfferDto>> GetUserOfferByGuid(Guid id)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());

        var userProfileWithOffer = await _unitOfWork.UserProfileRepository.GetUserProfileByOfferGuid(id);

        if (userProfile.Id != userProfileWithOffer.Id)
            return NotFound(new ApiResponse(404, "The offer not found"));
        
        return Ok(_mapper.Map<UserOfferDto>(await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(id)));
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddUserOffer(CreateUserOfferDto userOfferDto)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());

        var userOffer = new UserOffer
        {
            Preferences = userOfferDto.Preferences,
            UserProfile = userProfile,
            UserProfileId = userProfile.Id
        };
        
        userProfile.Offers.Add(userOffer);

        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to create the offer"));
    }
    
    [HttpPut("update")]
    public async Task<ActionResult> UpdateUserOffer(UserOfferDto userOfferDto)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());
        
        var userProfileWithOffer = await _unitOfWork.UserProfileRepository.GetUserProfileByOfferGuid(userOfferDto.Id);
        
        if (userProfile.Id != userProfileWithOffer.Id)
            return NotFound(new ApiResponse(404, "The offer not found"));

        var userOffer = await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(userOfferDto.Id);

        userOffer.Preferences = userOfferDto.Preferences;
        
        if (await _unitOfWork.Complete())
            return Ok();

        return BadRequest(new ApiResponse(400, "Failed to update the offer"));
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult> DeleteUserOffer(Guid id)
    {
        var userProfile =
            await _unitOfWork.UserProfileRepository.GetUserProfileByUsernameWithOffersIncludeAsync(User.GetUsername());
        
        var userProfileWithOffer = await _unitOfWork.UserProfileRepository.GetUserProfileByOfferGuid(id);
        
        if (userProfile.Id != userProfileWithOffer.Id)
            return NotFound(new ApiResponse(404, "The offer not found"));

        var userOffer = await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(id);
        
        _unitOfWork.Repository<UserOffer>().Delete(userOffer);

        if (await _unitOfWork.Complete())
            return Ok();
        
        return BadRequest(new ApiResponse(400, "Failed to delete the offer"));
    }
}