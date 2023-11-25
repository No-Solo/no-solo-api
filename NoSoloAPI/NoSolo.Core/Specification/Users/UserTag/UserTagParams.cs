using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.UserTag;

public class UserTagParams : BasicParams
{
    public Guid UserTagGuid { get; set; }
    public Guid? UserGuid { get; set; }

    public bool IsActive { get; set; } = true;
    public string? SortByAlphabetical { get; set; }

    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}