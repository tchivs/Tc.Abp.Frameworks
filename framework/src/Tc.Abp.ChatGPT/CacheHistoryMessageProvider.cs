﻿using Tc.Abp.ChatGPT.Models;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Tc.Abp.ChatGPT;

public class CacheHistoryMessageProvider : IHistoryMessageStore
{
    private readonly IDistributedCache<List<ChatGptMessage>> distributedCache;

    public CacheHistoryMessageProvider(Volo.Abp.Caching.IDistributedCache<List<ChatGptMessage>> distributedCache)
    {
        this.distributedCache = distributedCache;
    }
  

    public Task DeleteAsync(string id)
    {
        return distributedCache.RemoveAsync(id);
    }

    public Task<List<ChatGptMessage>> GetAsync(string id)
    {
        return distributedCache.GetAsync(id);
    }

    public Task SetAsync(string id, List<ChatGptMessage> message, TimeSpan messageExpiration)
    {
        return distributedCache.SetAsync(id, message, new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions() { SlidingExpiration = messageExpiration });
    }
}