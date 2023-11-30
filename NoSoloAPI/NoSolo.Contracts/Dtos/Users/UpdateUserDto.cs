using System.ComponentModel.DataAnnotations;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Core.Enums;

namespace NoSolo.Contracts.Dtos.Users;

public class UpdateUserDto : BaseDto
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }

    public string About { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    
    public LocaleEnum Locale { get; set; }
    public GenderEnum Gender { get; set; }
    public SponsorshipEnum Sponsorship { get; set; }
}