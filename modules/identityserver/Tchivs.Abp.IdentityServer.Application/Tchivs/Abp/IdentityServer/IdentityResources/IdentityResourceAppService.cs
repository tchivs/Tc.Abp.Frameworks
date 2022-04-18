using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tchivs.Abp.IdentityServer;
using Tchivs.Abp.IdentityServer.IdentityResources;
using Tchivs.Abp.IdentityServer.IdentityResources.Dtos;
using Tchivs.Abp.IdentityServer.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Tchivs.Abp.IdentityServer.Mappers.IdentityResources
{
    [Authorize(Policy = IdentityServerPermissions.IdentityResources.Default)]
    public class IdentityResourceAppService :
        CrudAppService<IdentityResource, PagingIdentityResourceListOutput, Guid, PagingIdentityResourceListInput,
            CreateIdentityResourceInput, UpdateIdentityResourceInput>, IIdentityResourceAppService
    {
        private readonly IdentityResourceManager _identityResourceManager;

        public IdentityResourceAppService(IdentityResourceManager identityResourceManager,
            IRepository<IdentityResource, Guid> repository) : base(repository)
        {
            _identityResourceManager = identityResourceManager;
        }


        public async Task<List<PagingIdentityResourceListOutput>> GetAllAsync()
        {
            var list = await _identityResourceManager.GetAllAsync();
            return ObjectMapper.Map<List<IdentityResource>, List<PagingIdentityResourceListOutput>>(list);
        }


        protected override string GetPolicyName { get; set; } =
            IdentityServerPermissions.IdentityResources.Default;

        protected override string UpdatePolicyName { get; set; } =
            IdentityServerPermissions.IdentityResources.Update;

        protected override string DeletePolicyName { get; set; } =
            IdentityServerPermissions.IdentityResources.Delete;

        protected override string CreatePolicyName { get; set; } =
            IdentityServerPermissions.IdentityResources.Create;
    }
}