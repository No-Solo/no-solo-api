using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.FeedBack;
using NoSolo.Infrastructure.Data.DbContext;

namespace NoSolo.Infrastructure.Repositories.FeedBack;

public class FeedBackRepositories : IFeedBackRepository
{
    private readonly FeedBackContext _feedBackContext;

    public FeedBackRepositories(FeedBackContext feedBackContext)
    {
        _feedBackContext = feedBackContext;
    }

    public async Task<IReadOnlyList<Core.Entities.FeedBack.FeedBackEntity>> Get()
    {
        return await _feedBackContext.Set<Core.Entities.FeedBack.FeedBackEntity>()
            .ToListAsync();
    }

    public async Task<Core.Entities.FeedBack.FeedBackEntity> Get(Guid feedBackGuid)
    {
        return await _feedBackContext.Set<Core.Entities.FeedBack.FeedBackEntity>()
            .FindAsync(feedBackGuid);
    }

    public async Task Delete(Core.Entities.FeedBack.FeedBackEntity feedBackEntity)
    {
        _feedBackContext.Set<Core.Entities.FeedBack.FeedBackEntity>()
            .Remove(feedBackEntity);
    }

    public async void AddAsync(Core.Entities.FeedBack.FeedBackEntity feedBackEntity)
    {
        await _feedBackContext.Set<Core.Entities.FeedBack.FeedBackEntity>()
            .AddAsync(feedBackEntity);
    }

    public void Save()
    {
        if (_feedBackContext.ChangeTracker.HasChanges())
            _feedBackContext.SaveChanges();
    }
}