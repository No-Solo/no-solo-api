using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Enums;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Services;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services;

public class MemberService : IMemberService
{
    private readonly IUnitOfWork _unitOfWork;

    public MemberService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<OrganizationUser> MoreAsync(OrganizationUser member1, OrganizationUser member2)
    {
        if (member1.Role < member2.Role)
            return member1;
        else if (member1.Role > member2.Role)
            return member2;

        return member1;
    }

    public async Task<OrganizationUser> LessAsync(OrganizationUser member1, OrganizationUser member2)
    {
        if (member1.Role > member2.Role)
            return member1;
        else if (member1.Role < member2.Role)
            return member2;

        return member1;
    }

    public bool More(OrganizationUser member1, OrganizationUser member2)
    {
        if (member1.Role < member2.Role)
            return false;
        else if (member1.Role > member2.Role)
            return true;

        return true;
    }

    public bool Less(OrganizationUser member1, OrganizationUser member2)
    {
        if (member1.Role > member2.Role)
            return false;
        else if (member1.Role < member2.Role)
            return true;

        return true;
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

    public async Task<RoleEnum> GetMemberRoleAsync(Organization organization, User user)
    {
        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        return member.Role;
    }

    public async Task<RoleEnum> GetMemberRoleAsync(Organization organization, string username)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(username);
        
        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        return member.Role;
    }

    public async Task<bool> MemberHasRoleAsync(RoleEnum role, Organization organization, User user)
    {
        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return false;

        if (member.Role == role)
            return true;

        return false;
    }

    public async Task<bool> MemberHasRoleAsync(RoleEnum role, Organization organization, string username)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(username);
        
        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return false;

        if (member.Role == role)
            return true;

        return false;
    }

    public async Task<bool> MemberHasRolesAsync(IEnumerable<RoleEnum> roles, Organization organization, User user)
    {
        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return false;

        foreach (var role in roles)
        {
            if (member.Role == role)
                return true;   
        }

        return false;
    }

    public async Task<bool> MemberHasRolesAsync(IEnumerable<RoleEnum> roles, Organization organization, string username)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithMembersIncludeAsync(username);
        
        var member = user.OrganizationUsers.SingleOrDefault(x => x.OrganizationId == organization.Id && x.UserId == user.Id);

        if (member == null)
            return false;

        foreach (var role in roles)
        {
            if (member.Role == role)
                return true;   
        }

        return false;
    }
}