using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Auth;
using NoSolo.Core.Entities.Auth;
using NoSolo.Infrastructure.Data.DbContext;

namespace NoSolo.Infrastructure.Repositories.Auth;

public class RefreshTokenRepository(DataBaseContext dataBaseContext) : IRefreshTokenRepository
{
    public async Task<RefreshToken> GetActiveByUserId(Guid userId)
    {
        return await dataBaseContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ExpiryDate >= DateTime.UtcNow);
    }
}