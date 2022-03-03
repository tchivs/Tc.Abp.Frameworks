using System.Threading.Tasks;

namespace Tchivs.Abp.UI.Toolbars;

public interface IToolbarManager
{
    Task<Toolbar> GetAsync(string name);
}
