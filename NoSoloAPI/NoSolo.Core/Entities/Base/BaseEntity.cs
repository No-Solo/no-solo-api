using System.ComponentModel.DataAnnotations;

namespace NoSolo.Abstractions.Base;

public class BaseEntity
{
    [Key] public Guid Id { get; set; }
}