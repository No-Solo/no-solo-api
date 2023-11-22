using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.UserContact;

public class UserContactParams : BasicParams
{
    public Guid? UserProfileId { get; set; }
    
    public string? SortByAlphabetical { get; set; }
    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}