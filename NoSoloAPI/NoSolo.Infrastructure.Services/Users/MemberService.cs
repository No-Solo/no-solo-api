using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;

namespace NoSolo.Infrastructure.Services.Users;

public class MemberService(IUserService userService, IRepository<MemberEntity> repository)
    : IMemberService
{
    public async Task CreateMember(OrganizationEntity organizationEntity, UserEntity userEntity, RoleEnum role)
    {
        var member = new MemberEntity
        {
            Role = role,
            UserEntity = userEntity,
            UserId = userEntity.Id,
            OrganizationEntity = organizationEntity,
            OrganizationId = organizationEntity.Id
        };

        repository.AddAsync(member);
        repository.Save();
    }

    public async Task AddMember(OrganizationEntity organizationEntity, UserEntity userEntity, RoleEnum role)
    {
        if (!await MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationEntity.Id,
                userEntity.Email!))
            throw new NotAccessException();

        if (!await MemberHasRole(RoleEnum.None, organizationEntity.Id, userEntity.Email!))
            throw new BadRequestException("The userEntity is already in the organizationEntity");

        await CreateMember(organizationEntity, userEntity, role);
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

    public async Task<MemberEntity> GetMember(string email, Guid organizationGuid)
    {
        var user = await userService.GetUser(email, UserInclude.Membership);
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
        repository.Save();
    }

    public async Task Delete(string email, string targetEmail, Guid organizationGuid)
    {
        if (!await MemberHasRoles(
                new List<RoleEnum>() { RoleEnum.Owner, RoleEnum.Administrator, RoleEnum.Moderator }, organizationGuid,
                email))
            throw new NotAccessException();

        if (await MemberHasRole(RoleEnum.None, organizationGuid, targetEmail))
            throw new BadRequestException("The organizationEntity hasn't this userEntity");

        var removingMember = await GetMember(targetEmail, organizationGuid);
        var member = await GetMember(email, organizationGuid);

        if (!await More(member.Role, removingMember.Role))
            throw new NotAccessException();

        repository.Delete(removingMember);
        repository.Save();
    }
}