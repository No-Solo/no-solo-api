using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Project;

public class ProjectWithFiltersForCountSpecification : BaseSpecification<Entities.Organization.Project>
{
    public ProjectWithFiltersForCountSpecification(ProjectParams projectParams)
    : base(x => (string.IsNullOrEmpty(projectParams.Search) || x.Description.Contains(projectParams.Search))
    && (projectParams.OrganizationId.HasValue || x.OrganizationId == projectParams.OrganizationId))
    {
        
    }
}