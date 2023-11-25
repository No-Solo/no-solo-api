using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;

namespace NoSolo.Infrastructure.Services.Users;

public class MemberService : IMemberService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public MemberService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
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
}