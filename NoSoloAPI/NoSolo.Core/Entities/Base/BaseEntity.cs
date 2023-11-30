using System.ComponentModel.DataAnnotations;

namespace NoSolo.Core.Entities.Base;

public class BaseEntity
{
    [Key] public Guid Id { get; set; }
}