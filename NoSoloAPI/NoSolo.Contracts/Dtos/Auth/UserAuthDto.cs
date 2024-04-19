using NoSolo.Contracts.Dtos.Users;

namespace NoSolo.Contracts.Dtos.Auth;

public record UserAuthDto
{
    
    public required UserDto User { get; set; }
    public required TokensDto Tokens { get; set; }
}