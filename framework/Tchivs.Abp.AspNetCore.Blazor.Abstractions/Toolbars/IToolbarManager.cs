using System.Threading.Tasks;

namespace Tchivs.Abp.AspNetCore.Blazor.Abstractions
{
    public interface IToolbarManager
    {
        Task<Toolbar> GetAsync(string name);
    }
}
