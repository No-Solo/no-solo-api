using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Contracts.Dtos.User;

namespace NoSolo.Contracts.Dtos.Users;

public class UserAuthDto
{
    public UserDto User { get; set; }
    public TokensDto Tokens { get; set; }
}