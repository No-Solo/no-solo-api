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

    public async Task<IReadOnlyList<Core.Entities.FeedBack.FeedBack>> Get()
    {
        return await _feedBackContext.Set<Core.Entities.FeedBack.FeedBack>()
            .ToListAsync();
    }

    public async Task<Core.Entities.FeedBack.FeedBack> Get(Guid feedBackGuid)
    {
        return await _feedBackContext.Set<Core.Entities.FeedBack.FeedBack>()
            .FindAsync(feedBackGuid);
    }

    public async Task Delete(Core.Entities.FeedBack.FeedBack feedBack)
    {
        _feedBackContext.Set<Core.Entities.FeedBack.FeedBack>()
            .Remove(feedBack);
    }

    public async void AddAsync(Core.Entities.FeedBack.FeedBack feedBack)
    {
        await _feedBackContext.Set<Core.Entities.FeedBack.FeedBack>()
            .AddAsync(feedBack);
    }

    public void Save()
    {
        if (_feedBackContext.ChangeTracker.HasChanges())
            _feedBackContext.SaveChanges();
    }
}