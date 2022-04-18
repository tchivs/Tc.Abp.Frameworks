

using Volo.Abp.Application.Dtos;

namespace Tchivs.Abp.IdentityServer.Dtos
{
        public class PagingApiRseourceListInput : PagedAndSortedResultRequestDto
        {
            public string Filter { get; set; }
        }
}