using Tc.Abp.ChatGPT.Models;

namespace Tc.Abp.ChatGPT;

/// <summary>
/// conversation manager
/// </summary>
public interface IConversationManager
{
    /// <summary>
    /// Update the chat history of a conversation.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the new conversation.</param>
    /// <param name="messages">The history message.</param>
    /// <param name="message">The system message.</param>
    /// <returns></returns>
    Task UpdateHistory(Guid conversationId, IList<ChatGptMessage> messages, ChatGptMessage message);
    async Task UpdateHistory(Guid conversationId, ChatGptMessage message)
    {
        var messages = await GetAsync(conversationId);
        await UpdateHistory(conversationId, messages, message);
    }

    /// <summary>
    /// Setups a new conversation with a system message, that is used to influence assistant behavior.
    /// </summary>
    /// <param name="message">The system message.</param>
    /// <returns>The unique identifier of the new conversation.</returns>
    /// <remarks>This method creates a new conversation with a system message and a random Conversation Id. Then, call  with this Id to start the actual conversation.</remarks>
    /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
    Task<Guid> SetupAsync(string message)
        => SetupAsync(Guid.NewGuid(), message);

    /// <summary>
    /// Setups a conversation with a system message, that is used to influence assistant behavior.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the conversation, used to automatically retrieve previous messages in the chat history.</param>
    /// <param name="message">The system message.</param>
    /// <remarks>This method creates a new conversation, with a system message and the given <paramref name="conversationId"/>. If a conversation with this Id already exists, it will be automatically cleared. Then, call to start the actual conversation.</remarks>
    /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
    Task<Guid> SetupAsync(Guid conversationId, string message);

    /// <summary>
    /// Retrieves a chat conversation from the cache.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the conversation.</param>
    /// <returns>The message list of the conversation, or <see cref="Enumerable.Empty{ChatGptMessage}"/> if the Conversation Id does not exist.</returns>
    /// <seealso cref="ChatGptMessage"/>
    Task<List<ChatGptMessage>> GetAsync(Guid conversationId);

    /// <summary>
    /// Loads messages into a new conversation.
    /// </summary>
    /// <param name="messages">Messages to load into a new conversation.</param>
    /// <returns>The unique identifier of the new conversation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="messages"/> is <see langword="null"/>.</exception>
    /// <remarks>
    /// <para>This method creates a new conversation with a random Conversation Id. Then, call  with this Id to start the actual conversation.</para>
    /// <para>The total number of messages never exceeds the message limit defined in <see cref="ChatGptOptions.MessageLimit"/>. If <paramref name="messages"/> contains more, only the latest ones are loaded.</para>
    /// </remarks>
    /// <seealso cref="ChatGptOptions.MessageLimit"/>
    Task<Guid> LoadAsync(IEnumerable<ChatGptMessage> messages)
        => LoadAsync(Guid.NewGuid(), messages);

    /// <summary>
    /// Loads messages into conversation history.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the conversation.</param>
    /// <param name="messages">The messages to load into conversation history.</param>
    /// <param name="replaceHistory"><see langword="true"/> to replace all the existing messages; <see langword="false"/> to mantain them.</param>
    /// <returns>The unique identifier of the conversation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="messages"/> is <see langword="null"/>.</exception>
    /// <remarks>The total number of messages never exceeds the message limit defined in <see cref="ChatGptOptions.MessageLimit"/>. If <paramref name="messages"/> contains more, only the latest ones are loaded.</remarks>
    /// <seealso cref="ChatGptOptions.MessageLimit"/>
    Task<Guid> LoadAsync(Guid conversationId, IEnumerable<ChatGptMessage> messages, bool replaceHistory = true);

    /// <summary>
    /// Deletes a chat conversation, clearing all the history.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the conversation.</param>
    /// <param name="preserveSetup"><see langword="true"/> to maintain the system message that has been specified with the <see cref="SetupAsync(Guid, string)"/> method.</param>
    /// <returns>The <see cref="Task"/> corresponding to the asynchronous operation.</returns>
    /// <seealso cref="SetupAsync(Guid, string)"/>
    Task DeleteAsync(Guid conversationId, bool preserveSetup = false);
}
