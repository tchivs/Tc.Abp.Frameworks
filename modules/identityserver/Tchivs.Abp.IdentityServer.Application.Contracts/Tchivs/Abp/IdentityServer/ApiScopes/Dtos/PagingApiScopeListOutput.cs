using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Tchivs.Abp.IdentityServer.ApiScopes.Dtos
{
    public class PagingApiScopeListOutput : EntityDto<Guid>
    {
        [Display(Name="启用")]
        public bool Enabled { get; set; }
        [Display(Name="名称")][Required]
        public string Name { get; set; }
        [Display(Name="显示名称")]
        public string DisplayName { get; set; }
        [Display(Name="备注")]
        public string Description { get; set; }
        [Display(Name="必须")]
        public bool Required { get; set; }

        public bool Emphasize { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }
    }
}