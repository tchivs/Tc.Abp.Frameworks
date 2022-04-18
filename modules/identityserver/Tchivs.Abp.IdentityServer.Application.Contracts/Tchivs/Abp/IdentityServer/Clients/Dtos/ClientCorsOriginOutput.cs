using System;
using Volo.Abp.Application.Dtos;

namespace Tchivs.Abp.IdentityServer.Clients
{
    public class ClientCorsOriginOutput
    {
        public Guid ClientId { get; set; }

        public string Origin { get; set; }
    }
}