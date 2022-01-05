namespace Tchivs.Abp.AspNetCore.Blazor.Abstractions
{
    public static class ResourceConst
    {
        public static readonly string Default = "Tchivs.Abp.AspNetCore.Blazor.Abstractions";
        public static readonly string[] Css = new[] {
            $"_content/{Default}/libs/@fortawesome/fontawesome-free/css/all.css",
            $"_content/{Default}/libs/@fortawesome/fontawesome-free/css/v4-shims.css",
            $"_content/{Default}/libs/abp/core/abp.css"};
        public static readonly string[] Js = new[] { 
            $"_content/{Default}/libs/abp/core/abp.js" ,
            $"_content/{Default}/js/account-proxy.js",
            "_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/abp.js"
        };
    }
}