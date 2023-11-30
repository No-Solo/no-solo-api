using AutoMapper;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Base.Create;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationContact;
using NoSolo.Core.Specification.OrganizationContact;
using NoSolo.Core.Specification.Users.UserContact;

namespace NoSolo.Infrastructure.Services.Contacts;

public class ContactService : IContactService
{
    private readonly IGenericRepository<Contact<Organization>> _organizationContactRepository;
    private readonly IGenericRepository<Contact<User>> _userContactRepository;
    private readonly IMapper _mapper;

    public ContactService(IGenericRepository<Contact<Organization>> organizationContactRepository,
        IGenericRepository<Contact<User>> userContactRepository, IMapper mapper)
    {
        _organizationContactRepository = organizationContactRepository;
        _userContactRepository = userContactRepository;
        _mapper = mapper;
    }

    public async Task<ContactDto> Add(Organization organization, NewContactDto contactDto)
    {
        var contact = new Contact<Organization>
        {
            Type = contactDto.Type,
            Text = contactDto.Text,
            Url = contactDto.Url,
            TEntity = organization,
            TEntityId = organization.Id
        };

        _organizationContactRepository.AddAsync(contact);

        _organizationContactRepository.Save();

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<ContactDto> Add(User user, NewContactDto contactDto)
    {
        var contact = new Contact<User>
        {
            Type = contactDto.Type,
            Text = contactDto.Text,
            Url = contactDto.Url,
            TEntity = user,
            TEntityId = user.Id
        };

        _userContactRepository.AddAsync(contact);
        _userContactRepository.Save();

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<Contact<Organization>> Get(Organization organization, Guid contactGuid)
    {
        var contact = organization.Contacts.SingleOrDefault(c => c.Id == contactGuid);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        return contact;
    }

    public async Task<ContactDto> GetDto(Organization organization, Guid contactGuid)
    {
        return _mapper.Map<ContactDto>(Get(organization, contactGuid));
    }

    public async Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams)
    {
        var spec = new OrganizationContactWithSpecificationParams(organizationContactParams);

        var countSpec = new OrganizationContactWithFiltersForCountSpecification(organizationContactParams);

        var totalItems = await _organizationContactRepository.CountAsync(countSpec);

        var organizationContacts = await _organizationContactRepository.ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Contact<Organization>>, IReadOnlyList<ContactDto>>(organizationContacts);

        return new Pagination<ContactDto>(organizationContactParams.PageNumber, organizationContactParams.PageSize,
            totalItems, data);
    }

    public async Task<Pagination<ContactDto>> Get(UserContactParams userContactParams)
    {
        var spec = new UserContactWithSpecificationParams(userContactParams);

        var countSpec = new UserContactWithFiltersForCountSpecification(userContactParams);

        var totalItems = await _userContactRepository.CountAsync(countSpec);

        var userContacts = await _userContactRepository.ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Contact<User>>, IReadOnlyList<ContactDto>>(userContacts);

        return new Pagination<ContactDto>(userContactParams.PageNumber, userContactParams.PageSize, totalItems, data);
    }

    public async Task<Contact<User>> Get(User user, Guid contactGuid)
    {
        var contact = user.Contacts.SingleOrDefault(c => c.Id == contactGuid);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        return contact;
    }

    public async Task<ContactDto> GetDto(User user, Guid contactGuid)
    {
        return _mapper.Map<ContactDto>(Get(user, contactGuid));
    }

    public async Task<ContactDto> Update(Organization organization, ContactDto contactDto)
    {
        var contact = await Get(organization, contactDto.Id);

        _mapper.Map(contactDto, contact);
        _organizationContactRepository.Save();

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<ContactDto> Update(User user, ContactDto contactDto)
    {
        var contact = user.Contacts.FirstOrDefault(c => c.Id == contactDto.Id);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        _mapper.Map(contactDto, contact);
        
        _userContactRepository.Save();

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task Delete(Organization organization, Guid contactGuid)
    {
        var contact = await Get(organization, contactGuid);

        _organizationContactRepository.Delete(contact);

        _organizationContactRepository.Save();
    }

    public async Task Delete(User user, Guid contactGuid)
    {
        var contact = await Get(user, contactGuid);

        _userContactRepository.Delete(contact);

        _organizationContactRepository.Save();
    }
}