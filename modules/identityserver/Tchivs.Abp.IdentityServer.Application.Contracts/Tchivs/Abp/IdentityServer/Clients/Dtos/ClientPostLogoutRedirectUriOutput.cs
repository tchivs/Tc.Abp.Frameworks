using System;
using Volo.Abp.Application.Dtos;

namespace Tchivs.Abp.IdentityServer.Clients
{
    public class ClientPostLogoutRedirectUriOutput
    {
        public Guid ClientId { get; set; }

        public string PostLogoutRedirectUri { get; set; }
    }
}