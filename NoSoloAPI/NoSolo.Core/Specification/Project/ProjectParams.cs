using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Project;

public class ProjectParams : BasicParams
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