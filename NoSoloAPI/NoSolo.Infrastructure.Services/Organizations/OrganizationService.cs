using AutoMapper;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Abstractions.Services.Utility.Pagination;
using NoSolo.Contracts.Dtos.Organizations.Organizations;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Organization.Organization;

namespace NoSolo.Infrastructure.Services.Organizations;

public class OrganizationService : IOrganizationService
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
        _user ??= await _userService.GetUser(email, UserInclude.Membership);

        var organization = new Organization()
        {
            Name = organizationDto.Name,
            Description = organizationDto.Description,
            NumberOfEmployees = organizationDto.NumberOfEmployees,
            Address = organizationDto.Address,
            WebSiteUrl = organizationDto.WebSiteUrl
        };

        await _memberService.CreateMember(organization, _user, RoleEnum.Owner);

        return _mapper.Map<OrganizationDto>(organization);
    }

    public async Task<OrganizationDto> AddMember(Guid organizationId, string email, string targetEmail)
    {
        var organization = await Get(organizationId, OrganizationIncludeEnum.Members);

        var user = await _userService.GetUser(targetEmail, UserInclude.Membership);

        await _memberService.AddMember(organization, user, RoleEnum.Member);

        return _mapper.Map<OrganizationDto>(organization);
    }

    public async Task RemoveMember(Guid organizationGuid, string email, string targetEmail)
    {
        await _memberService.Delete(email, targetEmail, organizationGuid);
    }

    public async Task UpdateRoleForMember(Guid organizationGuid, string email, string targetEmail, RoleEnum newRole)
    {
        await _memberService.UpdateRoleForMember(email, targetEmail, organizationGuid, newRole);
    }

    public async Task<Pagination<OrganizationDto>> GetMy(Guid userGuid)
    {
        var organizationParams = new OrganizationParams()
        {
            Includes = new List<OrganizationIncludeEnum>()
            {
                OrganizationIncludeEnum.Contacts,
                OrganizationIncludeEnum.Members,
                OrganizationIncludeEnum.Offers,
                OrganizationIncludeEnum.Photos
            },
            UserGuid = userGuid
        };
        
        return await Get(organizationParams);
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
        organization.LastUpdated = DateTime.UtcNow;
        
        await _unitOfWork.Complete();

        return _mapper.Map<OrganizationDto>(organization);
    }

    public async Task Delete(Guid organizationGuid, string email)
    {
        if (!await _memberService.MemberHasRoles(new List<RoleEnum>() { RoleEnum.Owner }, organizationGuid, email))
            throw new NotAccessException();

        var organization = await Get(organizationGuid, OrganizationIncludeEnum.Members);

        _unitOfWork.Repository<Organization>().Delete(organization);
        await _unitOfWork.Complete();
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

        return await GetOrganizationByParams(organizationParams);
    }

    public async Task<Organization> Get(Guid organizationGuid, List<OrganizationIncludeEnum> includes)
    {
        var organizationParams = new OrganizationParams()
        {
            OrganizationGuid = organizationGuid,
            Includes = includes
        };

        return await GetOrganizationByParams(organizationParams);
    }

    private async Task<Organization> GetOrganizationByParams(OrganizationParams organizationParams)
    {
        var spec = new OrganizationWithSpecificationParams(organizationParams);

        var organization = await _unitOfWork.Repository<Organization>().GetEntityWithSpec(spec);
        if (organization is null)
            throw new EntityNotFound("The organization is not found");

        return organization;
    }
}