﻿using Tc.Abp.ChatGPT.Exceptions;
using Tc.Abp.ChatGPT.Models;

namespace Tc.Abp.ChatGPT;

/// <summary>
/// Provides methods to interact with ChatGPT.
/// </summary>
public interface IChatGptClient
{


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

     
}
