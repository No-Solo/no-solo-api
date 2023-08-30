using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public RefreshTokenRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<RefreshToken?> GetActiveByUserId(Guid userId)
    {
        return await _dataBaseContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ExpiryDate >= DateTime.UtcNow);
    }
}