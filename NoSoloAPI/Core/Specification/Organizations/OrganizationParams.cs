namespace Core.Specification.Organizations;

public class OrganizationParams : BasicParams
{
    private int _pageSize = 6;
    
    public bool WithOffers { get; set; }
    public bool WithPhotos { get; set; }
    public bool WithContacts { get; set; }
    public bool WithMembers { get; set; }
    
    public string? SortByAlphabetical { get; set; }
    public string? SortByDate { get; set; }
    
    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}