﻿using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Repositories.Repositories;
using NoSolo.Core.Entities.Auth;
using NoSolo.Infrastructure.Data.Data;

namespace NoSolo.Infrastructure.Repositories.Auth;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public RefreshTokenRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<RefreshToken> GetActiveByUserId(Guid userId)
    {
        return await _dataBaseContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ExpiryDate >= DateTime.UtcNow);
    }
}