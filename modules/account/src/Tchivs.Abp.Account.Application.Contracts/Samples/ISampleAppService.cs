using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tchivs.Abp.Account.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}
