using NoSolo.Core.Entities.Organization;

namespace NoSolo.Core.Entities.Base;

public class Contact<T> : BaseEntity where T : IBaseForContact
{
    public T TEntity { get; set; }
    public Guid TEntityId { get; set; }

    public string Type { get; set; }
    public string Url { get; set; }
    public string Text { get; set; }
}