using AutoMapper;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Base.Create;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Users.UserContact;

namespace NoSolo.Infrastructure.Services.Contacts;

public class UserContactService : IUserContactService
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    private User? _user;
    
    public UserContactService(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;

        _user = null;
    }
    
    public async Task<ContactDto> Add(NewContactDto contactDto, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Contacts);
        
        var contact = new Contact<User>
        {
            Type = contactDto.Type,
            Text = contactDto.Text,
            Url = contactDto.Url
        };

        _user.Contacts.Add(contact);

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<Pagination<ContactDto>> Get(UserContactParams userContactParams, Guid guid)
    {
        userContactParams.UserGuid = guid;

        return await GetUserContactsBySpecificationParams(userContactParams);
    }

    public async Task<ContactDto> Get(Guid contactGuid, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Contacts);

        var contact = _user.Contacts.FirstOrDefault(c => c.Id == contactGuid);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<ContactDto> Update(ContactDto contactDto, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Contacts);
        
        var contact = _user.Contacts.FirstOrDefault(c => c.Id == contactDto.Id);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        _mapper.Map(contactDto, contact);

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task Delete(Guid contactGuid, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Contacts);
        
        var contact = _user.Contacts.FirstOrDefault(c => c.Id == contactGuid);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        _user.Contacts.Remove(contact);
    }
    
    private async Task<Pagination<ContactDto>> GetUserContactsBySpecificationParams(UserContactParams userContactParams)
    {
        var spec = new UserContactWithSpecificationParams(userContactParams);

        var countSpec = new UserContactWithFiltersForCountSpecification(userContactParams);

        var totalItems = await _unitOfWork.Repository<Contact<User>>().CountAsync(countSpec);

        var userOffers = await _unitOfWork.Repository<Contact<User>>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Contact<User>>, IReadOnlyList<ContactDto>>(userOffers);

        return new Pagination<ContactDto>(userContactParams.PageNumber, userContactParams.PageSize, totalItems, data);
    }
}