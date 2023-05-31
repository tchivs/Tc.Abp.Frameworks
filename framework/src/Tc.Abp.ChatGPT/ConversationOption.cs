using Volo.Abp.DependencyInjection;

namespace Tc.Abp.ChatGPT;

public class ConversationOption:IScopedDependency
{
    public TimeSpan MessageExpiration { get;set;}=TimeSpan.FromHours(1);
    public int MessageLimit { get; set; }
}
