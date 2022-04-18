using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tchivs.Abp.IdentityServer;
using Tchivs.Abp.IdentityServer.ApiScopes.Dtos;
using Tchivs.Abp.IdentityServer.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.ApiScopes;

namespace Tchivs.Abp.IdentityServer.ApiScopes
{
    [Authorize(Policy = IdentityServerPermissions.ApiScope.Default)]
    public class ApiScopeAppService :
        CrudAppService<ApiScope, PagingApiScopeListOutput, Guid, PagingApiScopeListInput, CreateApiScopeInput,
            UpdateCreateApiScopeInput>, IApiScopeAppService
    {
        private readonly IdenityServerApiScopeManager _idenityServerApiScopeManager;
        private readonly IdentityResourceManager _identityResourceManager;

        public ApiScopeAppService(IdenityServerApiScopeManager idenityServerApiScopeManager,
            IdentityResourceManager identityResourceManager, IRepository<ApiScope, Guid> repository) : base(repository)
        {
            _idenityServerApiScopeManager = idenityServerApiScopeManager;
            _identityResourceManager = identityResourceManager;
        }


        protected override string GetPolicyName { get; set; } =
            IdentityServerPermissions.ApiScope.Default;

        protected override string UpdatePolicyName { get; set; } =
            IdentityServerPermissions.ApiScope.Update;

        protected override string DeletePolicyName { get; set; } =
            IdentityServerPermissions.ApiScope.Delete;

        protected override string CreatePolicyName { get; set; } =
            IdentityServerPermissions.ApiScope.Create;


        public async Task<List<KeyValuePair<string, string>>> FindAllAsync()
        {
            var result = new List<KeyValuePair<string, string>>();
            var apiScopes = await _idenityServerApiScopeManager.FindAllAsync();
            result.AddRange(apiScopes.Select(e => new KeyValuePair<string, string>(e.Name, e.DisplayName)).ToList());
            var identityResoure = await _identityResourceManager.GetAllAsync();
            result.AddRange(identityResoure.Select(e => new KeyValuePair<string, string>(e.Name, e.DisplayName))
                .ToList());
            return result;
        }
    }
}