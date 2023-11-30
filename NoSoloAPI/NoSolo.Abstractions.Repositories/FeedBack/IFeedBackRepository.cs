namespace NoSolo.Abstractions.Repositories.FeedBack;

public interface IFeedBackRepository
{
    Task<IReadOnlyList<Core.Entities.FeedBack.FeedBack>> Get();
    Task<Core.Entities.FeedBack.FeedBack> Get(Guid feedBackGuid);
    Task Delete(Core.Entities.FeedBack.FeedBack feedBack);

    void AddAsync(Core.Entities.FeedBack.FeedBack feedBack);
    void Save();
}