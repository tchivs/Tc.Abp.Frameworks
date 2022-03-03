using System.Threading.Tasks;

namespace Tchivs.Abp.UI.PageToolbars;

public interface IPageToolbarContributor
{
    Task ContributeAsync(PageToolbarContributionContext context);
}
