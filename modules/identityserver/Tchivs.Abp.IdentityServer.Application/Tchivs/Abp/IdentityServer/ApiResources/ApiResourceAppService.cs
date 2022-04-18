using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tchivs.Abp.IdentityServer.Dtos;
using Tchivs.Abp.IdentityServer.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.ApiResources;

namespace Tchivs.Abp.IdentityServer.ApiResources
{
    [Authorize(Policy =  IdentityServerPermissions.ApiResource.Default)]
    public class ApiResourceAppService :
        CrudAppService<ApiResource, ApiResourceOutput, Guid, PagingApiRseourceListInput, CreateApiResourceInput,
            UpdateApiResourceInput>, IApiResourceAppService
    {
        private readonly IdenityServerApiResourceManager _idenityServerApiResourceManager;

        public ApiResourceAppService(IdenityServerApiResourceManager idenityServerApiResourceManager,
            IRepository<ApiResource, Guid> repository) : base(repository)
        {
            _idenityServerApiResourceManager = idenityServerApiResourceManager;
        }


        /// <summary>
        /// 获取所有api resource
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApiResourceOutput>> GetApiResources()
        {
            var list = await _idenityServerApiResourceManager.GetResources(false);
            return ObjectMapper.Map<List<ApiResource>, List<ApiResourceOutput>>(list);
        }

        protected override string GetPolicyName { get; set; } =
            IdentityServerPermissions.ApiResource.Default;

        protected override string UpdatePolicyName { get; set; } =
            IdentityServerPermissions.ApiResource.Update;

        protected override string DeletePolicyName { get; set; } =
            IdentityServerPermissions.ApiResource.Delete;

        protected override string CreatePolicyName { get; set; } =
            IdentityServerPermissions.ApiResource.Create;
    }
}