namespace NoSolo.Core.Specification.BaseSpecification;

public class BasicParams
{
    private const int MaxPageSize = 50;
    private int _pageSize = 9;
    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}