using System.ComponentModel.DataAnnotations;
using NoSolo.Core.Enums;

namespace NoSolo.Core.Entities.Base;

public class RequestEntity<T, U> : BaseEntity<Guid> where T : class where U : BaseEntity<Guid>
{
    public T? TEntity { get; set; }
    
    [Required]
    public required Guid TEntityId { get; set; }

    [Required]
    public required StatusEnum Status { get; set; }

    public U? UEntity { get; set; }
    
    [Required]
    public required Guid UEntityId { get; set; }
}