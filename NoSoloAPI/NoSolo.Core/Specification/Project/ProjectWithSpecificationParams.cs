using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Project;

public class ProjectWithSpecificationParams : BaseSpecification<Entities.Organization.Project>
{
    public ProjectWithSpecificationParams(ProjectParams projectParams)
        : base(x => (string.IsNullOrEmpty(projectParams.Search) || x.Description.Contains(projectParams.Search))
                    && (projectParams.OrganizationId.HasValue || x.OrganizationId == projectParams.OrganizationId))
    {
        ApplyPaging(projectParams.PageSize * (projectParams.PageNumber -1), projectParams.PageSize);
        
        if (!string.IsNullOrEmpty(projectParams.SortByAlphabetical))
        {
            switch (projectParams.SortByAlphabetical)
            {
                case "alphaAsc":
                    AddOrderBy(p => p.Description);
                    break;
                case "alphaDesc":
                    AddOrderByDescending(p => p.Description);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
        }
    }
}