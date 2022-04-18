using Volo.Abp.Application.Dtos;

namespace Tchivs.Abp.IdentityServer.ApiScopes.Dtos
{
    public class PagingApiScopeListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}