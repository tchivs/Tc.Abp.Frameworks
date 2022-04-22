using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
namespace Tchivs.Abp.UI
{
    [DependsOn(typeof(TchivsAbpUIModule))]
    public class AbpUIAntDesignModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAntDesign();
            /*
        _content/AntDesign/css/ant-design-blazor.css
        _content/AntDesign/js/ant-design-blazor.js
             */
        }
    }
}