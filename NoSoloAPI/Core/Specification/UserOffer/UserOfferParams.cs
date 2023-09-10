namespace Core.Specification.UserOffer;

public class UserOfferParams : BasicParams
{
    public Guid? UserProfileId { get; set; }
    public string? SortByAlphabetical { get; set; }
    public string? SortByDate { get; set; }
    
    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}