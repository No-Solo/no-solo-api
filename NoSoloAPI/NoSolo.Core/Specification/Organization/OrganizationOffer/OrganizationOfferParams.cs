using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.OrganizationOffer;

public class OrganizationOfferParams : BasicParams
{
    public Guid? OrganizationId { get; set; }
    public string? SortByAlphabetical { get; set; }
    public string? SortByDate { get; set; }

    public List<string>? Tags { get; set; }
    
    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}