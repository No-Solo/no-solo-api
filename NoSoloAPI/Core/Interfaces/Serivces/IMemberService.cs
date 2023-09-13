using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Serivces;

public interface IMemberService
{
    Task<OrganizationUser> MoreAsync(OrganizationUser member1, OrganizationUser member2);
    Task<OrganizationUser> LessAsync(OrganizationUser member1, OrganizationUser member2);
    bool More(OrganizationUser member1, OrganizationUser member2);
    bool Less(OrganizationUser member1, OrganizationUser member2);

    RoleEnum ParseRole(string role);
    
    Task<RoleEnum> GetMemberRoleAsync(Organization organization, User user);
    Task<RoleEnum> GetMemberRoleAsync(Organization organization, string username);
    
    Task<bool> MemberHasRoleAsync(RoleEnum role, Organization organization, User user);
    Task<bool> MemberHasRoleAsync(RoleEnum role, Organization organization, string username);
    Task<bool> MemberHasRolesAsync(IEnumerable<RoleEnum> roles, Organization organization, User user);
    Task<bool> MemberHasRolesAsync(IEnumerable<RoleEnum> roles, Organization organization, string username);
}