using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Abstractions.Services.Memberships;

public interface IMemberService
{
    RoleEnum ParseRole(string role);
    Task<bool> MemberHasRoles(List<RoleEnum> roles, Guid organizationGuid, string email);
    Task<bool> MemberHasRole(RoleEnum role, Guid organizationGuid, string email);
    Task<bool> More(RoleEnum first, RoleEnum second);
    Task<Member> GetMember(string email, Guid organizationGuid);
}