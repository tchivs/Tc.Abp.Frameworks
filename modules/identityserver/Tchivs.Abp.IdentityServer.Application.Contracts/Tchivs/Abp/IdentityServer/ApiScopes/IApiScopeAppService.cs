using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tchivs.Abp.IdentityServer.ApiScopes.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tchivs.Abp.IdentityServer.ApiScopes
{
    public interface IApiScopeAppService : ICrudAppService<PagingApiScopeListOutput,Guid,PagingApiScopeListInput,CreateApiScopeInput,UpdateCreateApiScopeInput>
    {
       
        Task<List<KeyValuePair<string, string>>> FindAllAsync();
    }
}