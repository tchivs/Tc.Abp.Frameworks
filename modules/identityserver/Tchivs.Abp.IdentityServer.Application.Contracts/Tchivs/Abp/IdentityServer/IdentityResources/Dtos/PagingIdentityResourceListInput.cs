using Volo.Abp.Application.Dtos;

namespace Tchivs.Abp.IdentityServer.IdentityResources.Dtos
{
    public class PagingIdentityResourceListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}