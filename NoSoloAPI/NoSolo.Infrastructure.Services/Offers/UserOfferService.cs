using AutoMapper;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organization;
using NoSolo.Contracts.Dtos.User;
using NoSolo.Contracts.Dtos.User.Create;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationOffer;
using NoSolo.Core.Specification.User.UserOffer;
using NoSolo.Core.Specification.Users.UserOffer;

namespace NoSolo.Infrastructure.Services.Offers;

public class UserOfferService : IUserOfferService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    private User? _user;
    
    public UserOfferService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = userService;

        _user = null;
    }

    public async Task<UserOfferDto> Add(NewUserOfferDto userOfferDto, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Offers);

        var userOffer = new UserOffer()
        {
            Preferences = userOfferDto.Preferences
        };
        
        _user.Offers.Add(userOffer);

        return _mapper.Map<UserOfferDto>(userOffer);
    }
    

    public async Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams)
    {
        return await GetUserOffersBySpecificationParams(userOfferParams);
    }

    public async Task<Pagination<UserOfferDto>> Get(UserOfferParams userOfferParams, Guid guid)
    {
        userOfferParams.UserGuid = guid;

        return await GetUserOffersBySpecificationParams(userOfferParams);
    }

    public async Task<UserOfferDto> GetUserOffer(Guid offerGuid)
    {
        var offer = await _unitOfWork.Repository<UserOffer>().GetByGuidAsync(offerGuid);

        return _mapper.Map<UserOfferDto>(offer);
    }

    public async Task<UserOfferDto> Update(UserOfferDto userOfferDto, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Offers);

        var offer = _user.Offers.FirstOrDefault(o => o.Id == userOfferDto.Id);
        if (offer is null)
            throw new EntityNotFound("The offer is not found");
        
        _mapper.Map(userOfferDto, offer);

        return _mapper.Map<UserOfferDto>(offer);
    }

    public async Task Delete(Guid offerGuid, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Offers);
        
        var offer = _user.Offers.FirstOrDefault(o => o.Id == offerGuid);
        if (offer is null)
            throw new EntityNotFound("The offer is not found");

        _user.Offers.Remove(offer);
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