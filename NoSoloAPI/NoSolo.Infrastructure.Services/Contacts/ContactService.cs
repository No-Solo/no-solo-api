using AutoMapper;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Contacts;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationContact;
using NoSolo.Core.Specification.Users.UserContact;

namespace NoSolo.Infrastructure.Services.Contacts;

public class ContactService(
    IRepository<ContactEntity<OrganizationEntity>> organizationContactRepository,
    IRepository<ContactEntity<UserEntity>> userContactRepository,
    IMapper mapper)
    : IContactService
{
    public ContactDto Add(OrganizationEntity organizationEntity, NewContactDto contactDto)
    {
        var contact = new ContactEntity<OrganizationEntity>
        {
            Type = contactDto.Type,
            Text = contactDto.Text,
            Url = contactDto.Url,
            TEntity = organizationEntity,
            TEntityId = organizationEntity.Id
        };

        organizationContactRepository.AddAsync(contact);

        organizationContactRepository.Save();

        return mapper.Map<ContactDto>(contact);
    }

    public ContactDto Add(UserEntity userEntity, NewContactDto contactDto)
    {
        var contact = new ContactEntity<UserEntity>
        {
            Type = contactDto.Type,
            Text = contactDto.Text,
            Url = contactDto.Url,
            TEntity = userEntity,
            TEntityId = userEntity.Id
        };

        userContactRepository.AddAsync(contact);
        userContactRepository.Save();

        return mapper.Map<ContactDto>(contact);
    }

    public ContactEntity<OrganizationEntity> Get(OrganizationEntity organizationEntity, Guid contactGuid)
    {
        var contact = organizationEntity.Contacts.SingleOrDefault(c => c.Id == contactGuid);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        return contact;
    }

    public ContactDto GetDto(OrganizationEntity organizationEntity, Guid contactGuid)
    {
        return mapper.Map<ContactDto>(Get(organizationEntity, contactGuid));
    }

    public async Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams)
    {
        var spec = new OrganizationContactWithSpecificationParams(organizationContactParams);

        var countSpec = new OrganizationContactWithFiltersForCountSpecification(organizationContactParams);

        var totalItems = await organizationContactRepository.CountAsync(countSpec);

        var organizationContacts = await organizationContactRepository.ListAsync(spec);

        var data = mapper
            .Map<IReadOnlyList<ContactEntity<OrganizationEntity>>, IReadOnlyList<ContactDto>>(organizationContacts);

        return new Pagination<ContactDto>(organizationContactParams.PageNumber, organizationContactParams.PageSize,
            totalItems, data);
    }

    public async Task<Pagination<ContactDto>> Get(UserContactParams userContactParams)
    {
        var spec = new UserContactWithSpecificationParams(userContactParams);

        var countSpec = new UserContactWithFiltersForCountSpecification(userContactParams);

        var totalItems = await userContactRepository.CountAsync(countSpec);

        var userContacts = await userContactRepository.ListAsync(spec);

        var data = mapper
            .Map<IReadOnlyList<ContactEntity<UserEntity>>, IReadOnlyList<ContactDto>>(userContacts);

        return new Pagination<ContactDto>(userContactParams.PageNumber, userContactParams.PageSize, totalItems, data);
    }

    public ContactEntity<UserEntity> Get(UserEntity userEntity, Guid contactGuid)
    {
        var contact = userEntity.Contacts.SingleOrDefault(c => c.Id == contactGuid);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        return contact;
    }

    public ContactDto GetDto(UserEntity userEntity, Guid contactGuid)
    {
        return mapper.Map<ContactDto>(Get(userEntity, contactGuid));
    }

    public ContactDto Update(OrganizationEntity organizationEntity, ContactDto contactDto)
    {
        var contact = Get(organizationEntity, contactDto.Id);

        mapper.Map(contactDto, contact);
        organizationContactRepository.Save();

        return mapper.Map<ContactDto>(contact);
    }

    public ContactDto Update(UserEntity userEntity, ContactDto contactDto)
    {
        var contact = userEntity.Contacts.FirstOrDefault(c => c.Id == contactDto.Id);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        mapper.Map(contactDto, contact);
        
        userContactRepository.Save();

        return mapper.Map<ContactDto>(contact);
    }

    public void Delete(OrganizationEntity organizationEntity, Guid contactGuid)
    {
        var contact = Get(organizationEntity, contactGuid);

        organizationContactRepository.Delete(contact);

        organizationContactRepository.Save();
    }

    public void Delete(UserEntity userEntity, Guid contactGuid)
    {
        var contact = Get(userEntity, contactGuid);

        userContactRepository.Delete(contact);

        organizationContactRepository.Save();
    }
}