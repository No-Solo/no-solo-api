namespace Core.Specification.UserTag;

public class UserTagParams : BasicParams
{
    public Guid? UserProfileId { get; set; }

    public bool IsActive { get; set; } = true;
    public string? SortByAlphabetical { get; set; }

    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}