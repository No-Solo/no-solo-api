using NoSolo.Core.Entities.Organization;

namespace NoSolo.Core.Entities.Base;

public class ContactEntity<T> : BaseEntity<Guid> where T : IBaseForContact
{
    public T? TEntity { get; set; }
    public required Guid TEntityId { get; set; }

    public required string Type { get; set; }
    public required string Url { get; set; }
    public required string Text { get; set; }
}