﻿using Tc.Abp.ChatGPT.Exceptions;
using Tc.Abp.ChatGPT.Models;

namespace Tc.Abp.ChatGPT;

/// <summary>
/// Options class that provides settings for configuring ChatGPT
/// </summary>
public class ChatGptOptions
{
    /// <summary>
    /// email
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }
    /// <summary>
    /// Gets or sets the API Key to access the service.
    /// </summary>
    /// <remarks>
    /// See <see href="https://help.openai.com/en/articles/4936850-where-do-i-find-my-secret-api-key">Where do I find my Secret API Key?</see> for more information.
    /// </remarks>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the HttpProxy but only init once.
    /// </summary>
    public string? Proxy { get; set; }

    /// <summary>
    /// Gets or sets the base-url by chatGPT api.
    /// </summary>
    public string BaseUrl { get; set; } = "https://api.openai.com/v1/";

    /// <summary>
    /// Gets or sets the maximum number of messages to use for chat completion (default: 10).
    /// </summary>
    public int MessageLimit { get; set; } = 10;

    /// <summary>
    /// Gets or sets the expiration for cached conversation messages (default: 1 hour).
    /// </summary>
    public TimeSpan MessageExpiration { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    /// Gets or sets a value that determines whether to throw a <see cref="ChatGptException"/> when an error occurred (default: <see langword="true"/>). If this property is set to <see langword="false"></see>, API errors are returned in the <see cref="ChatGptResponse"/> object.
    /// </summary>
    /// <see cref="ChatGptException"/>
    /// <seealso cref="ChatGptResponse"/>
    public bool ThrowExceptionOnError { get; set; } = true;

    /// <summary>
    /// Gets or sets a value that determines the organization the user belongs to.
    /// </summary>
    /// <remarks>For users who belong to multiple organizations, you can pass a header to specify which organization is used for an API request. Usage from these API requests will count against the specified organization's subscription quota.</remarks>
    public string? Organization { get; set; }

    /// <summary>
    /// Gets or sets the default model for chat completion. (default: <see cref="ChatGptModels.Gpt35Turbo"/>).
    /// </summary>
    /// <see cref="ChatGptModels"/>
    public string DefaultModel { get; set; } = ChatGptModels.Gpt35Turbo;

    /// <summary>
    ///  Gets or sets the default parameters for chat completion.
    /// </summary>
    /// <see cref="ChatGptParameters"/>
    public ChatGptParameters DefaultParameters { get; } = new();

    /// <summary>
    /// Gets or sets the user identification for chat completion, which can help OpenAI to monitor and detect abuse.
    /// </summary>
    /// <remarks>
    /// See <see href="https://platform.openai.com/docs/guides/safety-best-practices/end-user-ids">Safety best practices</see> for more information.
    /// </remarks>
    public string? User { get; set; }
}
