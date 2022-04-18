using Volo.Abp.Application.Dtos;

namespace Tchivs.Abp.IdentityServer.Clients
{
    public class PagingClientListInput:PagedResultRequestDto
    {
        public string Filter { get; set; }
    }
}