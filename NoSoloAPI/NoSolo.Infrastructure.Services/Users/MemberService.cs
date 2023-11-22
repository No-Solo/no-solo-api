using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;

namespace NoSolo.Infrastructure.Services.Users;

public class MemberService : IMemberService
{
    private readonly IUnitOfWork _unitOfWork;

    public MemberService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
}