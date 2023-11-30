using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;

namespace NoSolo.Infrastructure.Services.Users;

public class MemberService : IMemberService
{
    private readonly IUserService _userService;
    private readonly IGenericRepository<Member> _genericRepository;

    public MemberService(IUserService userService, IGenericRepository<Member> genericRepository)
    {
        _userService = userService;
        _genericRepository = genericRepository;
    }

    public async Task CreateMember(Organization organization, User user, RoleEnum role)
    {
        var member = new Member
        {
            Role = role,
            User = user,
            UserId = user.Id,
            Organization = organization,
            OrganizationId = organization.Id
        };

        _genericRepository.AddAsync(member);
        _genericRepository.Save();
    }

    public async Task AddMember(Organization organization, User user, RoleEnum role)
    {
        if (!await MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organization.Id,
                user.Email!))
            throw new NotAccessException();

        if (!await MemberHasRole(RoleEnum.None, organization.Id, user.Email!))
            throw new BadRequestException("The user is already in the organization");

        await CreateMember(organization, user, role);
    }

    public RoleEnum ParseRole(string role)
    {
        switch (role)
        {
            case "owner":
                return RoleEnum.Owner;
            case "administrator":
                return RoleEnum.Administrator;
            case "admin":
                return RoleEnum.Administrator;
            case "moderator":
                return RoleEnum.Moderator;
            case "moder":
                return RoleEnum.Moderator;
            case "member":
                return RoleEnum.Member;
            default:
                return RoleEnum.None;
        }
    }

    public async Task<bool> MemberHasRoles(List<RoleEnum> roles, Guid organizationGuid, string email)
    {
        var member = await GetMember(email, organizationGuid);

        foreach (var role in roles)
        {
            if (member.Role == role)
                return true;
        }

        return false;
    }

    public async Task<bool> MemberHasRole(RoleEnum role, Guid organizationGuid, string email)
    {
        var member = await GetMember(email, organizationGuid);

        if (member.Role == role)
            return true;

        return false;
    }

    public async Task<bool> More(RoleEnum first, RoleEnum second)
    {
        if (first > second)
            return true;

        return false;
    }

    public async Task<Member> GetMember(string email, Guid organizationGuid)
    {
        var user = await _userService.GetUser(email, UserInclude.Membership);
        var member = user.OrganizationUsers.SingleOrDefault(m => m.OrganizationId == organizationGuid);
        if (member is null)
            throw new EntityNotFound("The membership is not found");

        return member;
    }

    public async Task UpdateRoleForMember(string email, string targetEmail, Guid organizationGuid, RoleEnum newRole)
    {
        if (!await MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Administrator, RoleEnum.Moderator, RoleEnum.Owner }, organizationGuid,
                email))
            throw new NotAccessException();

        if (await MemberHasRole(RoleEnum.None, organizationGuid, targetEmail))
            throw new NotAccessException();

        var uMember = await GetMember(targetEmail, organizationGuid);
        var member = await GetMember(email, organizationGuid);

        if (!await More(member.Role, uMember.Role))
            throw new NotAccessException();

        if (member.Role == RoleEnum.Owner && newRole == RoleEnum.Owner)
            member.Role = RoleEnum.Administrator;

        uMember.Role = newRole;

        if (newRole == RoleEnum.None)
            await Delete(email, targetEmail, organizationGuid);
        _genericRepository.Save();
    }

    public async Task Delete(string email, string targetEmail, Guid organizationGuid)
    {
        if (!await MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationGuid,
                email))
            throw new NotAccessException();

        if (await MemberHasRole(RoleEnum.None, organizationGuid, targetEmail))
            throw new BadRequestException("The organization hasn't this user");

        var removingMember = await GetMember(targetEmail, organizationGuid);
        var member = await GetMember(email, organizationGuid);

        if (!await More(member.Role, removingMember.Role))
            throw new NotAccessException();

        _genericRepository.Delete(removingMember);
        _genericRepository.Save();
    }
}