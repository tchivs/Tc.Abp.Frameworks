using Nacos.V2;

namespace Tchivs.Abp.Nacos;
/// <summary>
/// nacos基础配置项
/// </summary>
public class AbpNacosOptions
{
    public static readonly string DefaultName = "nacos";
    /// <summary>
    /// 是否使用配置中心
    /// </summary>
    public bool UseConfig { get; set; } = true;
    /// <summary>
    /// 配置中心配置项节点名称
    /// </summary>
    public string ConfigSectionName { get; set; } = DefaultName;
    /// <summary>
    /// 是否使用服务中心
    /// </summary>
    public bool UseNaming { get; set; } = true;
    /// <summary>
    /// 服务中心配置项节点名称
    /// </summary>
    public string NamingSectionName { get; set; } = DefaultName;
    public Action<NacosSdkOptions>? ConfigConfigureAction { get; set; }
    public Action<NacosSdkOptions>? NamingConfigureAction { get; set; }

}