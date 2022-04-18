
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tchivs.Abp.IdentityServer.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.IdentityServer.Clients;

namespace Tchivs.Abp.IdentityServer.Clients
{
    [Authorize(Policy = IdentityServerPermissions.Client.Default)]
    public class IdentityServerClientAppService : IdentityServerAppServiceBase, IIdentityServerClientAppService
    {
        private readonly IdenityServerClientManager _idenityServerClientManager;


        public IdentityServerClientAppService(IdenityServerClientManager idenityServerClientManager)
        {

            _idenityServerClientManager = idenityServerClientManager;

        }


        public async Task<PagedResultDto<PagingClientListOutput>> GetListAsync(PagingClientListInput input)
        {
            var list = await _idenityServerClientManager.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Filter,
                true);
            var totalCount = await _idenityServerClientManager.GetCountAsync(input.Filter);
            return new PagedResultDto<PagingClientListOutput>(totalCount,
                ObjectMapper.Map<List<Client>, List<PagingClientListOutput>>(list));
        }

        /// <summary>
        /// 创建Client
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Policy = IdentityServerPermissions.Client.Create)]
        public Task CreateAsync(CreateClientInput input)
        {
            return _idenityServerClientManager.CreateAsync(input.ClientId, input.ClientName, input.Description, input.AllowedGrantTypes);
        }

        /// <summary>
        /// 删除client
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = IdentityServerPermissions.Client.Delete)]
        public Task DeleteAsync(Guid id)
        {
            return _idenityServerClientManager.DeleteAsync(id);
        }

        /// <summary>
        /// 更新基本信息
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = IdentityServerPermissions.Client.Update)]
        public Task UpdateBasicDataAsync(Guid id, UpdataBasicDataInput input)
        {
            return _idenityServerClientManager.UpdateBasicDataAsync(
                id,
                input.ClientId,
                input.ClientName,
                input.Description,
                input.ClientUri,
                input.LogoUri,
                input.Enabled,
                input.ProtocolType,
                input.RequireClientSecret,
                input.RequireConsent,
                input.AllowRememberConsent,
                input.AlwaysIncludeUserClaimsInIdToken,
                input.RequirePkce,
                input.AllowPlainTextPkce,
                input.RequireRequestObject,
                input.AllowAccessTokensViaBrowser,
                input.FrontChannelLogoutUri,
                input.FrontChannelLogoutSessionRequired,
                input.BackChannelLogoutUri,
                input.BackChannelLogoutSessionRequired,
                input.AllowOfflineAccess,
                input.IdentityTokenLifetime,
                input.AllowedIdentityTokenSigningAlgorithms,
                input.AccessTokenLifetime,
                input.AuthorizationCodeLifetime,
                input.ConsentLifetime,
                input.AbsoluteRefreshTokenLifetime,
                input.RefreshTokenUsage,
                input.UpdateAccessTokenClaimsOnRefresh,
                input.RefreshTokenExpiration,
                input.AccessTokenType,
                input.EnableLocalLogin,
                input.IncludeJwtId,
                input.AlwaysSendClientClaims,
                input.ClientClaimsPrefix,
                input.PairWiseSubjectSalt,
                input.UserSsoLifetime,
                input.UserCodeType,
                input.DeviceCodeLifetime,
                input.SlidingRefreshTokenLifetime,
                input.Secret,
                input.SecretType,
                input.AllowedGrantTypes
            );
        }

        /// <summary>
        /// 更新client scopes
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = IdentityServerPermissions.Client.Update)]
        public Task UpdateScopesAsync(Guid id, UpdateScopeInput scopes)
        {
            return _idenityServerClientManager.UpdateScopesAsync(id, scopes);
        }

        /// <summary>
        /// 新增回调地址
        /// </summary>
        [Authorize(Policy = IdentityServerPermissions.Client.Update)]
        public Task AddRedirectUriAsync(Guid id, string uri)
        {
            return _idenityServerClientManager.AddRedirectUriAsync(id, uri);
        }

        /// <summary>
        /// 删除回调地址
        /// </summary>
        [Authorize(Policy = IdentityServerPermissions.Client.Update)]
        public Task RemoveRedirectUriAsync(Guid id, string uri)
        {
            return _idenityServerClientManager.RemoveRedirectUriAsync(id, uri);
        }

        /// <summary>
        /// 新增Logout回调地址
        /// </summary>
        [Authorize(Policy = IdentityServerPermissions.Client.Update)]
        public Task AddLogoutRedirectUriAsync(Guid id, string uri)
        {
            return _idenityServerClientManager.AddLogoutRedirectUriAsync(id, uri);
        }

        /// <summary>
        /// 删除Logout回调地址
        /// </summary>
        [Authorize(Policy = IdentityServerPermissions.Client.Update)]
        public Task RemoveLogoutRedirectUriAsync(Guid id, string uri)
        {
            return _idenityServerClientManager.RemoveLogoutRedirectUriAsync(id, uri);
        }

        /// <summary>
        /// 添加cors
        /// </summary>
        [Authorize(Policy = IdentityServerPermissions.Client.Update)]
        public Task AddCorsAsync(Guid id, string origin)
        {
            return _idenityServerClientManager.AddCorsAsync(id, origin);
        }

        /// <summary>
        /// 删除cors
        /// </summary>
        [Authorize(Policy = IdentityServerPermissions.Client.Update)]
        public Task RemoveCorsAsync(Guid id, string origin)
        {
            return _idenityServerClientManager.RemoveCorsAsync(id, origin);
        }

        /// <summary>
        /// 禁用client
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Policy = IdentityServerPermissions.Client.Enable)]
        public Task EnabledAsync(Guid id, bool enabled)
        {
            return _idenityServerClientManager.EnabledAsync(id, enabled);
        }


    }
}