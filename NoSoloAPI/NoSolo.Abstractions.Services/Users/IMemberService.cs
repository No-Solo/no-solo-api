using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Abstractions.Services.Users;

public interface IMemberService
{
    RoleEnum ParseRole(string role);
}