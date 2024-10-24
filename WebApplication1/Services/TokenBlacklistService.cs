using Microsoft.Extensions.Caching.Memory;

namespace WebApplication1.Services;

public class TokenBlacklistService
{
    private readonly IMemoryCache _cache;

    public TokenBlacklistService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void BlacklistToken(string token, DateTime expiry)
    {
        _cache.Set(token, true, expiry);
    }

    public bool IsTokenBlacklisted(string token)
    {
        return _cache.TryGetValue(token, out _);
    }
}