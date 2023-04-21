using Microsoft.Extensions.Options;
using Tc.Abp.ChatGPT.Models;

namespace Tc.Abp.ChatGPT;

public interface IHistoryMessageStore
{
    Task SetAsync(Guid id, List<ChatGptMessage> message, TimeSpan messageExpiration) => SetAsync(id.ToString(), message, messageExpiration);
    Task SetAsync(string id, List<ChatGptMessage> message, TimeSpan messageExpiration);
    Task DeleteAsync(Guid id)=>DeleteAsync(id.ToString());
    Task DeleteAsync(string id);
    Task<List<ChatGptMessage>> GetAsync(Guid id) => GetAsync(id.ToString());
    Task<List<ChatGptMessage>> GetAsync(string id) ;
}
