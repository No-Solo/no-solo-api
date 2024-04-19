using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.FeedBack;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/feed-back")]
[ExcludeFromCodeCoverage]
public class FeedBackController : BaseApiController
{
    private readonly IFeedBackService _feedBackService;

    public FeedBackController(IFeedBackService feedBackService)
    {
        _feedBackService = feedBackService;
    }

    [HttpPost]
    public async Task<FeedBackDto> CreateFeedBack([FromBody] NewFeedBackDto newFeedBackDto)
    {
        return await _feedBackService.Create(newFeedBackDto);
    }

    [HttpGet]
    public async Task<IReadOnlyList<FeedBackDto>> GetFeedBacks()
    {
        return await _feedBackService.Get();
    }
    
    [HttpGet("{feedBackGuid:guid}")]
    public async Task<FeedBackDto> GetFeedBackByGuid(Guid feedBackGuid)
    {
        return await _feedBackService.Get(feedBackGuid);
    }

    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpDelete("{feedBackGuid:guid}")]
    public async Task DeleteFeeedBack(Guid feedBackGuid)
    {
        await _feedBackService.Delete(feedBackGuid);
    }
}