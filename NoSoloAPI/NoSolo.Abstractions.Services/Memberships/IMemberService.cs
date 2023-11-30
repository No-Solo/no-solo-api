using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Abstractions.Services.Memberships;

public interface IMemberService
{
    Task CreateMember(Organization organization, User user, RoleEnum role);
    Task AddMember(Organization organization, User user, RoleEnum role);
    RoleEnum ParseRole(string role);
    Task<bool> MemberHasRoles(List<RoleEnum> roles, Guid organizationGuid, string email);
    Task<bool> MemberHasRole(RoleEnum role, Guid organizationGuid, string email);
    Task<bool> More(RoleEnum first, RoleEnum second);
    Task<Member> GetMember(string email, Guid organizationGuid);
    Task UpdateRoleForMember(string email, string targetEmail, Guid organizationGuid, RoleEnum newRole);
    Task Delete(string email, string targetEmail, Guid organizationGuid);
}