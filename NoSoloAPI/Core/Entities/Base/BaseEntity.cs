using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class BaseEntity
{
    [Key] public Guid Id { get; set; }
}