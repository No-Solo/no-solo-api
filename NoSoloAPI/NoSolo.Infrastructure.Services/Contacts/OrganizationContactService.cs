using AutoMapper;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Base.Create;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.OrganizationContact;
using NoSolo.Core.Specification.OrganizationContact;

namespace NoSolo.Infrastructure.Services.Contacts;

public class OrganizationContactService : IOrganizationContactService
{
    private readonly IOrganizaitonService _organizaitonService;
    private readonly IMemberService _memberService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrganizationContactService(IOrganizaitonService organizaitonService, IMemberService memberService,
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _organizaitonService = organizaitonService;
        _memberService = memberService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ContactDto> Add(NewContactDto contactDto, Guid organizationGuid, string email)
    {
        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();
        
        var organization =
            await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);
        
        var contact = new Contact<Organization>
        {
            Type = contactDto.Type,
            Text = contactDto.Text,
            Url = contactDto.Url
        };

        organization.Contacts.Add(contact);

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<Pagination<ContactDto>> Get(OrganizationContactParams organizationContactParams,
        Guid organizationGuid)
    {
        organizationContactParams.OrganizationId = organizationGuid;

        return await GetOrganizationContactsBySpecificationParams(organizationContactParams);
    }

    public async Task<ContactDto> Get(Guid contactGuid, Guid organizationGuid)
    {
        var organization =
            await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);
        
        var contact = organization.Contacts.FirstOrDefault(c => c.Id == contactGuid);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<ContactDto> Update(ContactDto contactDto, Guid organizationGuid, string email)
    {
        var organization =
            await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        var contact = organization.Contacts.FirstOrDefault(c => c.Id == contactDto.Id);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        _mapper.Map(contactDto, contact);

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task Delete(Guid contactGuid, Guid organizationGuid, string email)
    {
        var organization =
            await _organizaitonService.Get(organizationGuid, OrganizationIncludeEnum.Contacts);

        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Owner },
                organizationGuid, email))
            throw new NotAccessException();

        var contact = organization.Contacts.FirstOrDefault(c => c.Id == contactGuid);
        if (contact is null)
            throw new EntityNotFound("The contact is not found");

        organization.Contacts.Remove(contact);
    }
    
    private async Task<Pagination<ContactDto>> GetOrganizationContactsBySpecificationParams(
        OrganizationContactParams organizationContactParams)
    {
        var spec = new OrganizationContactWithSpecificationParams(organizationContactParams);

        var countSpec = new OrganizationContactWithFiltersForCountSpecification(organizationContactParams);

        var totalItems = await _unitOfWork.Repository<Contact<Organization>>().CountAsync(countSpec);

        var userOffers = await _unitOfWork.Repository<Contact<Organization>>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Contact<Organization>>, IReadOnlyList<ContactDto>>(userOffers);

        return new Pagination<ContactDto>(organizationContactParams.PageNumber, organizationContactParams.PageSize,
            totalItems, data);
    }
}