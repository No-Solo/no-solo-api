namespace Core.Specification.Project;

public class ProjectWithFiltersForCountSpecification : BaseSpecification<Entities.Project>
{
    public ProjectWithFiltersForCountSpecification(ProjectParams projectParams)
    : base(x => (string.IsNullOrEmpty(projectParams.Search) || x.Description.Contains(projectParams.Search))
    && (projectParams.OrganizationId.HasValue || x.OrganizationId == projectParams.OrganizationId))
    {
        
    }
}