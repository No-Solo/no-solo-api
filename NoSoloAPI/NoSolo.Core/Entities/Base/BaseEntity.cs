using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NoSolo.Core.Entities.Base;

public class BaseEntity
{
    [JsonProperty("id")]
    [Key] public Guid Id { get; set; }
}

public class BaseCreatedEntity : BaseEntity
{
    [JsonProperty("deleted")]
    public bool Deleted { get; set; }

    [JsonProperty("datecreated")]
    public DateTime? DateCreated { get; set; }

    [JsonProperty("datedeleted")]
    public DateTime? DateDeleted { get; set; }

}