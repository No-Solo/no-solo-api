namespace NoSolo.Abstractions.Repositories.FeedBack;

public interface IFeedBackRepository
{
    Task<IReadOnlyList<Core.Entities.FeedBack.FeedBackEntity>> Get();
    Task<Core.Entities.FeedBack.FeedBackEntity> Get(Guid feedBackGuid);
    Task Delete(Core.Entities.FeedBack.FeedBackEntity feedBackEntity);

    void AddAsync(Core.Entities.FeedBack.FeedBackEntity feedBackEntity);
    void Save();
}