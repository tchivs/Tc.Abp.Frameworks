using Microsoft.Extensions.DependencyInjection;
using System;
using Tchivs.Abp.Account.Localization;
using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.Account.HttpApi
{
    [DependsOn(typeof(AbpAccountHttpApiModule))]

    public class TchivsAbpAccountHttpApiModule:AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<TchivsAccountResource>()
                    .AddBaseTypes(typeof(AccountResource));
            });
        }
    }
    }
