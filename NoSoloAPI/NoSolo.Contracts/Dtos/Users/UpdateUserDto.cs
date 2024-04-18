using NoSolo.Contracts.Dtos.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Users;

public record UpdateUserDto : BaseDto<Guid>
{
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }

    public required string About { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    
    public required LocaleEnum Locale { get; set; }
    public required GenderEnum Gender { get; set; }
    public required SponsorshipEnum Sponsorship { get; set; }
}