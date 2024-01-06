using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Recommendation;

public class RecommendParams : BasicParams
{
    private const int MaxPageSize = 5;
    private int _pageSize = 3;
    public new int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}