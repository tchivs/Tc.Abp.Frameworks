using System.Threading.Tasks;

namespace Tchivs.Abp.AspNetCore.Blazor.Abstractions
{
    public interface IToolbarContributor
    {
        Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
    }
}