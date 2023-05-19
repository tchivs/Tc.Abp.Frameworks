using Tc.Abp.ChatGPT.Models;
using Volo.Abp.DependencyInjection;
using Volo.Abp;
using Microsoft.Extensions.DependencyInjection;

namespace Tc.Abp.ChatGPT;

public class ConversationManager : IConversationManager,IServiceProviderAccessor, ITransientDependency
{
    TimeSpan messageExpiration => this.options.MessageExpiration;
    public IServiceProvider? ServiceProvider { get; set; }
    public IAbpLazyServiceProvider AbpLazyServiceProvider { get=> ServiceProvider!.GetRequiredService<IAbpLazyServiceProvider>();   }
    public IHistoryMessageStore historyMessageStore { get=> ServiceProvider!.GetRequiredService<IHistoryMessageStore>();   }
    public ConversationOption options { get=> ServiceProvider!.GetRequiredService<ConversationOption>(); }

    public async Task<Guid> SetupAsync(Guid conversationId, string message)
    {
        Check.NotNull(message, nameof(message));

        // Ensures that conversationId isn't empty.
        if (conversationId == Guid.Empty)
        {
            conversationId = Guid.NewGuid();
        }

        var messages = new List<ChatGptMessage>
        {
            new()
            {
                Role = ChatGptRoles.System,
                Content = message
            }
        };
        await historyMessageStore.SetAsync(conversationId, messages, messageExpiration);

        return conversationId;
    }
    
    public async Task DeleteAsync(Guid conversationId, bool preserveSetup = false)
    {
        if (!preserveSetup)
        {
            // We don't want to preserve setup message, so just deletes all the cache history.
            await historyMessageStore.DeleteAsync(conversationId);
        }
        else
        {
            var messages = await historyMessageStore.GetAsync(conversationId);
            if (messages == null) return;
            messages!.RemoveAll(m => m.Role != ChatGptRoles.System);
            await historyMessageStore.SetAsync(conversationId, messages, messageExpiration);
        }
    }

    public async Task<List<ChatGptMessage>> GetAsync(Guid conversationId)
    {
        var messages = await historyMessageStore.GetAsync(conversationId) ?? new List<ChatGptMessage>();
        return messages;
    }
    public async Task<Guid> LoadAsync(Guid conversationId, IEnumerable<ChatGptMessage> messages, bool replaceHistory = true)
    {
        ArgumentNullException.ThrowIfNull(messages);

        // Ensures that conversationId isn't empty.
        if (conversationId == Guid.Empty)
        {
            conversationId = Guid.NewGuid();
        }

        if (replaceHistory)
        {
            // If messages must replace history, just use the current list, discarding all the previously cached content.
            // If messages.Count() > ChatGptOptions.MessageLimit, the UpdateCache take care of taking only the last messages.
            await UpdateCache(conversationId, messages);
        }
        else
        {
            // Retrieves the current history and adds new messages.
            var conversationHistory = await historyMessageStore.GetAsync(conversationId) ?? new List<ChatGptMessage>();
            conversationHistory.AddRange(messages);

            // If messages total length > ChatGptOptions.MessageLimit, the UpdateCache take care of taking only the last messages.
            await UpdateCache(conversationId, conversationHistory);
        }

        return conversationId;
    }

    private async Task UpdateCache(Guid conversationId, IEnumerable<ChatGptMessage> messages)
    {
        // If the maximum number of messages has been reached, deletes the oldest ones.
        // Note: system message does not count for message limit.
        var conversation = messages.Where(m => m.Role != ChatGptRoles.System);

        if (conversation.Count() > options.MessageLimit)
        {
            conversation = conversation.TakeLast(options.MessageLimit);

            // If the first message was of role system, adds it back in.
            var firstMessage = messages.First();
            if (firstMessage.Role == ChatGptRoles.System)
            {
                conversation = conversation.Prepend(firstMessage);
            }

            messages = conversation.ToList();
        }

        await historyMessageStore.SetAsync(conversationId, messages.ToList(),this.messageExpiration);

    }

    public async Task UpdateHistory(Guid conversationId, IList<ChatGptMessage> messages, ChatGptMessage message)
    {
        messages.Add(message);
        await UpdateCache(conversationId, messages);
    }
}
