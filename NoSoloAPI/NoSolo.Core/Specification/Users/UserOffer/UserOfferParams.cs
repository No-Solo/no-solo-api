using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Users.UserOffer;

public class UserOfferParams : BasicParams
{
    private const int MaxPageSize = 10;
    private int _pageSize = 3;
    public new int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public Guid? UserGuid { get; set; }
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