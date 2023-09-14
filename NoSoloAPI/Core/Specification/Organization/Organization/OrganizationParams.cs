using Core.Specification.BaseSpecification;

namespace Core.Specification.Organization.Organization;

public class OrganizationParams : BasicParams
{
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