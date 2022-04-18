using System.Collections.Generic;

namespace Tchivs.Abp.IdentityServer.Dtos
{
    public class CreateApiResourceInput
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public string AllowedAccessTokenSigningAlgorithms { get; set; }

        public bool ShowInDiscoveryDocument { get; set; } = true;

        public string Secret { get; set; }
        public List<string> Scopes { get; set; } = new List<string>();

    }
}