using System.Threading.Tasks;

namespace Tchivs.Abp.UI.Toolbars;

public interface IToolbarContributor
{
    Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
}
