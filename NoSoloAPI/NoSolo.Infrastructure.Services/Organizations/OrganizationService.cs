using AutoMapper;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.Organization;
using NoSolo.Contracts.Dtos.Organization.Update;
using NoSolo.Contracts.Dtos.Organizations.Organizations;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.Organization;

namespace NoSolo.Infrastructure.Services.Organizations;

public class OrganizationService : IOrganizaitonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IMemberService _memberService;

    private User? _user;

    public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService,
        IMemberService memberService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = userService;
        _memberService = memberService;

        _user = null;
    }

    public async Task<OrganizationDto> Create(NewOrganizationDto organizationDto, string email)
    {
        _user ??= await _userService.GetUser(email, UserInclude.Membership );

        var organization = new Organization()
        {
            Name = organizationDto.Name,
            Description = organizationDto.Description,
            NumberOfEmployees = organizationDto.NumberOfEmployees,
            Address = organizationDto.Address,
            WebSiteUrl = organizationDto.WebSiteUrl
        };

        var member = new Member
        {
            Role = RoleEnum.Owner,
            User = _user,
            UserId = _user.Id,
            Organization = organization,
            OrganizationId = organization.Id
        };

        return _mapper.Map<OrganizationDto>(organization);
    }

    public async Task<OrganizationDto> AddMember(Guid organizationId, string email, string targetEmail)
    {
        if (!await _memberService.MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationId,
                email))
            throw new NotAccessException();

        var organization = await Get(organizationId, OrganizationIncludeEnum.Members);

        var user = await _userService.GetUser(targetEmail, UserInclude.Membership);

        if (!await _memberService.MemberHasRole(RoleEnum.None, organizationId, targetEmail))
            throw new BadRequestException("The user is already in the organization");

        var member = new Member()
        {
            Role = RoleEnum.Member,
            User = user,
            UserId = user.Id,
            Organization = organization,
            OrganizationId = organization.Id
        };

        return _mapper.Map<OrganizationDto>(organization);
    }

    public async Task RemoveMember(Guid organizationGuid, string email, string targetEmail)
    {
        if (!await _memberService.MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationGuid,
                email))
            throw new NotAccessException();

        if (await _memberService.MemberHasRole(RoleEnum.None, organizationGuid, targetEmail))
            throw new BadRequestException("The organization hasn't this user");

        var removingMember = await _memberService.GetMember(targetEmail, organizationGuid);
        var member = await _memberService.GetMember(email, organizationGuid);

        if (!await _memberService.More(member.Role, removingMember.Role))
            throw new NotAccessException();

        _unitOfWork.Repository<Member>().Delete(removingMember);
    }

    public async Task UpdateRoleForMember(Guid organizationGuid, string email, string targetEmail, RoleEnum newRole)
    {
        if (!await _memberService.MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Moderator, RoleEnum.Owner }, organizationGuid,
                email))
            throw new NotAccessException();

        if (await _memberService.MemberHasRole(RoleEnum.None, organizationGuid, targetEmail))
            throw new NotAccessException();

        var uMember = await _memberService.GetMember(targetEmail, organizationGuid);
        var member = await _memberService.GetMember(email, organizationGuid);
        
        if (!await _memberService.More(member.Role, uMember.Role))
            throw new NotAccessException();

        if (member.Role == RoleEnum.Owner && newRole == RoleEnum.Owner)
            member.Role = RoleEnum.Administrator;

        uMember.Role = newRole;
    }

    public async Task<OrganizationDto> Get(Guid organizationGuid)
    {
        var organizationParams = new OrganizationParams()
        {
            OrganizationGuid = organizationGuid,
            Includes = new List<OrganizationIncludeEnum>()
            {
                OrganizationIncludeEnum.Contacts,
                OrganizationIncludeEnum.Members,
                OrganizationIncludeEnum.Offers,
                OrganizationIncludeEnum.Photos
            }
        };

        var organization = await GetOrganizationByParams(organizationParams);

        return _mapper.Map<OrganizationDto>(organization);
    }

    public async Task<OrganizationDto> Update(UpdateOrganizationDto organizationDto, string email)
    {
        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator },
                organizationDto.Id, email))
            throw new NotAccessException();

        var organization = await Get(organizationDto.Id, OrganizationIncludeEnum.Members);

        _mapper.Map(organizationDto, organization);

        return _mapper.Map<OrganizationDto>(organization);
    }

    public async Task Delete(Guid organizationGuid, string email)
    {
        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Owner }, organizationGuid, email))
            throw new NotAccessException();

        var organization = await Get(organizationGuid, OrganizationIncludeEnum.Members);

        _unitOfWork.Repository<Organization>().Delete(organization);
    }

    public async Task<Pagination<OrganizationDto>> Get(OrganizationParams organizationParams)
    {
        var spec = new OrganizationWithSpecificationParams(organizationParams);

        var countSpec = new OrganizationWithFiltersForCountSpecification(organizationParams);

        var totalItems = await _unitOfWork.Repository<Organization>().CountAsync(countSpec);

        var organizations = await _unitOfWork.Repository<Organization>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Organization>, IReadOnlyList<OrganizationDto>>(organizations);

        return new Pagination<OrganizationDto>(organizationParams.PageNumber, organizationParams.PageSize, totalItems,
            data);
    }

    public async Task<Organization> Get(Guid organizationGuid, OrganizationIncludeEnum include)
    {
        var organizationParams = new OrganizationParams()
        {
            OrganizationGuid = organizationGuid,
            Includes = new List<OrganizationIncludeEnum>() { include }
        };

        var organization = await GetOrganizationByParams(organizationParams);
        if (organization is null)
            throw new EntityNotFound("The organization is not found");

        return organization;
    }

    public async Task<Organization> Get(Guid organizationGuid, List<OrganizationIncludeEnum> includes)
    {
        var organizationParams = new OrganizationParams()
        {
            OrganizationGuid = organizationGuid,
            Includes = includes
        };

        var organization = await GetOrganizationByParams(organizationParams);
        if (organization is null)
            throw new EntityNotFound("The organization is not found");

        return organization;
    }

    private async Task<Organization> GetOrganizationByParams(OrganizationParams organizationParams)
    {
        var spec = new OrganizationWithSpecificationParams(organizationParams);

        return await _unitOfWork.Repository<Organization>().GetEntityWithSpec(spec);
    }
}