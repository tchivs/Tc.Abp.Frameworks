using System;

namespace Tchivs.Abp.IdentityServer.Dtos
{
    public class ApiResourceSecretOutput
    {
     
        public string Type { get;  set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public DateTime? Expiration { get; set; }
    }
}