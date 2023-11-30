using NoSolo.Core.Enums;
using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.Organization;

public class OrganizationParams : BasicParams
{
    public Guid? OrganizationGuid { get; set; }
    public Guid? UserGuid { get; set; }
    
    public List<OrganizationIncludeEnum>? Includes { get; set; }
    
    public string? SortByAlphabetical { get; set; }
    public string? SortByDate { get; set; }
    
    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}