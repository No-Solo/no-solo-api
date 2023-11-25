using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.UserOffer;

public class UserOfferParams : BasicParams
{
    public Guid? UserGuid { get; set; }
    public string? SortByAlphabetical { get; set; }
    public string? SortByDate { get; set; }
    
    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}