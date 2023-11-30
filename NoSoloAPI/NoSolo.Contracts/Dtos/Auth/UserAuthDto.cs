using NoSolo.Contracts.Dtos.Users;

namespace NoSolo.Contracts.Dtos.Auth;

public class UserAuthDto
{
    public UserDto User { get; set; }
    public TokensDto Tokens { get; set; }
}