using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace NacosWebApplication.Controllers
{
    [ApiController,Route("test")]

    public class TestService: AbpControllerBase
    {
        [HttpGet]
        public async Task<string> GetVersionAsync()
        {
            return "1.0";
        }
    }
}
