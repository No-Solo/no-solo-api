using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NoSolo.Core.Entities.Base;

public abstract class BaseEntity<TKey>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey Id { get; set; } = default!;
}

public class BaseCreatedEntity<TKey> : BaseEntity<TKey>
{
    [JsonProperty("deleted")]
    [Required]
    public required bool Deleted { get; set; }

    [Required]
    [JsonProperty("datecreated")]
    public required DateTime DateCreated { get; set; }

    [JsonProperty("datedeleted")]
    public DateTime? DateDeleted { get; set; }

}