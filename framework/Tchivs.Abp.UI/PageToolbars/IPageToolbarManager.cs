using System.Threading.Tasks;

namespace Tchivs.Abp.UI.PageToolbars;

public interface IPageToolbarManager
{
    Task<PageToolbarItem[]> GetItemsAsync(PageToolbar toolbar);
}
