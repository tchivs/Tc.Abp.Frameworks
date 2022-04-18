using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tchivs.Abp.IdentityServer.Clients
{
    public interface IIdentityServerClientAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询Client
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<PagingClientListOutput>> GetListAsync(PagingClientListInput input);

        /// <summary>
        /// 创建Client
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateClientInput input);

        /// <summary>
        /// 删除client
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 更新基本信息
        /// </summary>
        /// <returns></returns>
        Task UpdateBasicDataAsync(Guid id, UpdataBasicDataInput input);

        /// <summary>
        /// 更新client scopes
        /// </summary>
        /// <returns></returns>
        Task UpdateScopesAsync(Guid id, UpdateScopeInput input);

        /// <summary>
        /// 新增回调地址
        /// </summary>
        Task AddRedirectUriAsync(Guid id, string uri);

        /// <summary>
        /// 删除回调地址
        /// </summary>
        Task RemoveRedirectUriAsync(Guid id, string uri);

        /// <summary>
        /// 新增Logout回调地址
        /// </summary>
        Task AddLogoutRedirectUriAsync(Guid id, string uri);

        /// <summary>
        /// 删除Logout回调地址
        /// </summary>
        Task RemoveLogoutRedirectUriAsync(Guid id, string uri);

        /// <summary>
        /// 添加cors
        /// </summary>
        Task AddCorsAsync(Guid id, string origin);

        /// <summary>
        /// 删除cors
        /// </summary>
        Task RemoveCorsAsync(Guid id, string origin);

        /// <summary>
        /// 禁用client
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task EnabledAsync(Guid id, bool enabled);
    }
}
