# Tc.Abp.ChatGPT

## 集成

Tc.Abp.ChatGPT集成了ChatGPT接口调用，同时支持Blazor。

## 1、安装

将其安装到你的项目中(在分层应用程序中适用于 数据访问/基础设施层):

```shell
Install-Package Tc.Abp.ChatGPT
```

然后添加 `AbpDapperModule` 模块依赖项(DependsOn Attribute) 到 module(项目中的Mudole类):

```c#
using Tc.Abp.ChatGPT;
using Volo.Abp.Modularity;

namespace MyCompany.MyProject
{
    [DependsOn(typeof(TcAbpChatGPTModule))]
    public class MyModule : AbpModule
    {
        //...
    }
}

```

## 2、配置

### GPT配置

对应配置类为`ChatGptOptions`

#### 默认配置

默认从IConfiguration中读取ChatGPT节点：

打开项目的`appsettings.json`文件，添加以下配置：

```json
  "ChatGPT": {
    "Proxy": "socks5://127.0.0.1:1080",
    "ApiKey": "sk-xxx",
    "BaseUrl": "https://api.openapi.com/v1/"
  }
```

#### 自定义配置提供者

我们可以根据用户不同使用不同的GPT key和模型类型
只需要实现`IChatGptOptionsFactory`接口。

```c#
   public class DefaultChatGptOptionsFactory : IChatGptOptionsFactory, ISingletonDependency
    {
        private readonly IConfiguration configuration;
        private readonly ChatGptOptions options;

        public DefaultChatGptOptionsFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
            options = new ChatGptOptions();
            string sectionName = "ChatGPT";
            configuration.GetSection(sectionName).Bind(options);
        }
        public ChatGptOptions GetOptions()
        {
            return options;
        }
    }
```

## 使用

注入`IChatGptClient`
