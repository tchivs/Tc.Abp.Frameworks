﻿using Tc.Abp.ChatGPT.Exceptions;
using Tc.Abp.ChatGPT.Models;

namespace Tc.Abp.ChatGPT;

/// <summary>
/// Provides methods to interact with ChatGPT.
/// </summary>
public interface IChatGptClient
{
    /// <summary>
    /// Setups a new conversation with a system message, that is used to influence assistant behavior.
    /// </summary>
    /// <param name="message">The system message.</param>
    /// <returns>The unique identifier of the new conversation.</returns>
    /// <remarks>This method creates a new conversation with a system message and a random Conversation Id. Then, call <see cref="AskAsync(Guid, string, ChatGptParameters, string, CancellationToken)"/> with this Id to start the actual conversation.</remarks>
    /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
    /// <seealso cref="AskAsync(Guid, string, ChatGptParameters, string, CancellationToken)"/>
    Task<Guid> SetupAsync(string message)
        => SetupAsync(Guid.NewGuid(), message);

    /// <summary>
    /// Setups a conversation with a system message, that is used to influence assistant behavior.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the conversation, used to automatically retrieve previous messages in the chat history.</param>
    /// <param name="message">The system message.</param>
    /// <remarks>This method creates a new conversation, with a system message and the given <paramref name="conversationId"/>. If a conversation with this Id already exists, it will be automatically cleared. Then, call <see cref="AskAsync(Guid, string, ChatGptParameters, string, CancellationToken)"/> to start the actual conversation.</remarks>
    /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
    /// <seealso cref="AskAsync(Guid, string, ChatGptParameters, string, CancellationToken)"/>
    Task<Guid> SetupAsync(Guid conversationId, string message);

    /// <summary>
    /// Requests a new chat interaction using the default completion model specified in the <see cref="ChatGptOptions.DefaultModel"/> property.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="parameters">A <see cref="ChatGptParameters"/> object used to override the default completion parameters in the <see cref="ChatGptOptions.DefaultParameters"/> property.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The chat completion response.</returns>
    /// <remarks>This method automatically starts a new conservation with a random Conversation Id, that will be returned in the <see cref="ChatGptResponse"/>. Subsequent calls to this method must provide the same <see cref="ChatGptResponse.ConversationId"/> value, so that previous messages will be automatically used to continue the conversation.</remarks>
    /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
    /// <exception cref="ChatGptException">An error occurred while calling the API and the <see cref="ChatGptOptions.ThrowExceptionOnError"/> is <see langword="true"/>.</exception>
    /// <seealso cref="ChatGptRequest"/>
    /// <seealso cref="ChatGptResponse"/>
    /// <seealso cref="ChatGptOptions"/>
    /// <seealso cref="ChatGptParameters"/>
    Task<ChatGptResponse> AskAsync(string message, ChatGptParameters? parameters = null, CancellationToken cancellationToken = default) =>
        AskAsync(Guid.NewGuid(), message, parameters, null, cancellationToken);

    /// <summary>
    /// Requests a chat interaction.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the conversation, used to automatically retrieve previous messages in the chat history.</param>
    /// <param name="message">The message.</param>
    /// <param name="parameters">A <seealso cref="ChatGptParameters"/> object used to override the default completion parameters in the <see cref="ChatGptOptions.DefaultParameters"/> property.</param>
    /// <param name="model">The chat completion model to use. If model is <see langword="null"/>, then the one specified in the <see cref="ChatGptOptions.DefaultModel"/> property will be used.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The chat completion response.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
    /// <exception cref="ChatGptException">An error occurred while calling the API and the <see cref="ChatGptOptions.ThrowExceptionOnError"/> is <see langword="true"/>.</exception>
    /// <seealso cref="ChatGptRequest"/>
    /// <seealso cref="ChatGptResponse"/>
    /// <seealso cref="ChatGptParameters"/>
    Task<ChatGptResponse> AskAsync(Guid conversationId, string message, ChatGptParameters? parameters = null, string? model = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Requests a new chat interaction (using the default completion model specified in the <see cref="ChatGptOptions.DefaultModel"/> property) with streaming response, like in ChatGPT.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="parameters">A <see cref="ChatGptParameters"/> object used to override the default completion parameters in the <see cref="ChatGptOptions.DefaultParameters"/> property.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>An <see cref="IAsyncEnumerable{ChatGptResponse}"/> that allows to enumerate all the streaming responses, each of them containing a partial message delta.</returns>
    /// <remarks>
    /// This method automatically starts a new conservation with a random Conversation Id, that will be returned in the <see cref="ChatGptResponse"/>. Subsequent calls to this method must provide the same <see cref="ChatGptResponse.ConversationId"/> value, so that previous messages will be automatically used to continue the conversation.
    /// When using steaming, partial message deltas will be sent. Tokens will be sent as data-only server-sent events as they become available.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
    /// <exception cref="ChatGptException">An error occurred while calling the API and the <see cref="ChatGptOptions.ThrowExceptionOnError"/> is <see langword="true"/>.</exception>
    /// <seealso cref="ChatGptRequest"/>
    /// <seealso cref="ChatGptResponse"/>
    /// <seealso cref="ChatGptParameters"/>
    IAsyncEnumerable<ChatGptResponse> AskStreamAsync(string message, ChatGptParameters? parameters = null, CancellationToken cancellationToken = default) =>
        AskStreamAsync(Guid.NewGuid(), message, parameters, null, cancellationToken);

    /// <summary>
    /// Requests a chat interaction with streaming response, like in ChatGPT.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the conversation, used to automatically retrieve previous messages in the chat history.</param>
    /// <param name="message">The message.</param>
    /// <param name="parameters">A <see cref="ChatGptParameters"/> object used to override the default completion parameters in the <see cref="ChatGptOptions.DefaultParameters"/> property.</param>
    /// <param name="model">The chat completion model to use. If model is <see langword="null"/>, then the one specified in the <see cref="ChatGptOptions.DefaultModel"/> property will be used.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>An <see cref="IAsyncEnumerable{ChatGptResponse}"/> that allows to enumerate all the streaming responses, each of them containing a partial message delta.</returns>
    /// <remarks> When using steaming, partial message deltas will be sent. Tokens will be sent as data-only server-sent events as they become available.</remarks>
    /// <exception cref="ArgumentNullException"><paramref name="message"/> is <see langword="null"/>.</exception>
    /// <exception cref="ChatGptException">An error occurred while calling the API and the <see cref="ChatGptOptions.ThrowExceptionOnError"/> is <see langword="true"/>.</exception>
    /// <seealso cref="ChatGptRequest"/>
    /// <seealso cref="ChatGptResponse"/>
    /// <seealso cref="ChatGptParameters"/>
    IAsyncEnumerable<ChatGptResponse> AskStreamAsync(Guid conversationId, string message, ChatGptParameters? parameters = null, string? model = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a chat conversation from the cache.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the conversation.</param>
    /// <returns>The message list of the conversation, or <see cref="Enumerable.Empty{ChatGptMessage}"/> if the Conversation Id does not exist.</returns>
    /// <seealso cref="ChatGptMessage"/>
    Task<IEnumerable<ChatGptMessage>> GetConversationAsync(Guid conversationId);

    /// <summary>
    /// Loads messages into a new conversation.
    /// </summary>
    /// <param name="messages">Messages to load into a new conversation.</param>
    /// <returns>The unique identifier of the new conversation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="messages"/> is <see langword="null"/>.</exception>
    /// <remarks>
    /// <para>This method creates a new conversation with a random Conversation Id. Then, call <see cref="AskAsync(Guid, string, ChatGptParameters, string, CancellationToken)"/> with this Id to start the actual conversation.</para>
    /// <para>The total number of messages never exceeds the message limit defined in <see cref="ChatGptOptions.MessageLimit"/>. If <paramref name="messages"/> contains more, only the latest ones are loaded.</para>
    /// </remarks>
    /// <seealso cref="ChatGptOptions.MessageLimit"/>
    /// <seealso cref="AskStreamAsync(Guid, string, ChatGptParameters?, string?, CancellationToken)"/>
    Task<Guid> LoadConversationAsync(IEnumerable<ChatGptMessage> messages)
        => LoadConversationAsync(Guid.NewGuid(), messages);

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
    Task<Guid> LoadConversationAsync(Guid conversationId, IEnumerable<ChatGptMessage> messages, bool replaceHistory = true);

    /// <summary>
    /// Deletes a chat conversation, clearing all the history.
    /// </summary>
    /// <param name="conversationId">The unique identifier of the conversation.</param>
    /// <param name="preserveSetup"><see langword="true"/> to maintain the system message that has been specified with the <see cref="SetupAsync(Guid, string)"/> method.</param>
    /// <returns>The <see cref="Task"/> corresponding to the asynchronous operation.</returns>
    /// <seealso cref="SetupAsync(Guid, string)"/>
    Task DeleteConversationAsync(Guid conversationId, bool preserveSetup = false);
}
