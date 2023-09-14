using Core.Specification.BaseSpecification;

namespace Core.Specification.OrganizationContact;

public class OrganizationContactParams : BasicParams
{
    public Guid? OrganizationId { get; set; }
    
    public string? SortByAlphabetical { get; set; }
    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}