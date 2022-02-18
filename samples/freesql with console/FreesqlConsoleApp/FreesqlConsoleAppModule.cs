// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Tchivs.Abp.FreeSql;
using FreeSql.DataAnnotations;

namespace FreesqlConsoleApp;

[DependsOn(typeof(AbpAutofacModule), typeof(Tchivs.Abp.FreeSql.TchivsAbpFreeSqlModule))]
public class FreesqlConsoleAppModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();
        context.Services.AddHostedService<FreesqlConsoleAppService>();
        this.Configure<Tchivs.Abp.FreeSql.ConnectionOptions>(configuration.GetSection(nameof(ConnectionOptions)));
    }
}
public class FreesqlConsoleAppService : BackgroundService
{
    private readonly IFreeSqlSelector freeSqlSelector;
    private readonly IFreeSql freeSql;

    public FreesqlConsoleAppService(IFreeSqlSelector freeSqlSelector,IFreeSql freeSql)
    {
        this.freeSqlSelector = freeSqlSelector;
        this.freeSql = freeSql;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var fsql = freeSqlSelector.GetFreeSql();
        if (freeSql==fsql)
        {
            Console.WriteLine("获取成功");
        }
        var user = new User()
        {
            Name = "test",
            Password = "pwd"
        };
        var result = freeSql.Insert<User>(user).ExecuteAffrows();
        if (result > 0)
        {
            Console.WriteLine("数据插入成功");
        }
        freeSql.Select<User>().ToList().ForEach(x => Console.WriteLine(x.Name));
    }
}
public class User
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
}