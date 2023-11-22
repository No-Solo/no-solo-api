using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Organization.Project;

public class ProjectDto : BaseDto
{
    public string? Name { get; set; }
    public string Description { get; set; }
}