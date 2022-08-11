using AntDesign.ProLayout;
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
            context.Services.Configure<ProSettings>(x =>
            {
                x.NavTheme = "dark";
                x.Layout = "side";
                x.ContentWidth = "Fluid";
                x.FixedHeader = false;
                x.FixSiderbar = true;
                x.Title = "Antd";
                x.PrimaryColor = "daybreak";
                x.ColorWeak = false;
                x.SplitMenus = false;
                x.HeaderRender = true;
                x.FooterRender = true;
                x.MenuRender = true;
                x.MenuHeaderRender = true;
                x.HeaderHeight = 48;
            });
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(AbpUIAntDesignModule).Assembly;
                options.DefaultLayout = typeof(Layouts.MainLayout);
            });
        }
    }
}