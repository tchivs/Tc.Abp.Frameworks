using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Tchivs.Abp.IdentityServer.Dtos
{
    public class ApiResourceOutput : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public string AllowedAccessTokenSigningAlgorithms { get; set; }

        public bool ShowInDiscoveryDocument { get; set; } = true;

        public List<ApiResourceSecretOutput> Secrets { get; set; }

        public List<string> Scopes { get; set; }

        public List<string> UserClaims { get; set; }

        public List<ApiResourcePropertyOutput> Properties { get; set; }
    }
}