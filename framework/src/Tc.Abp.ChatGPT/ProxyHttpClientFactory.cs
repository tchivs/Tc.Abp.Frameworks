using System.Net;
using Flurl.Http.Configuration;

namespace Tc.Abp.ChatGPT
{
    public class ProxyHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly ChatGptOptions options;
      
        public ProxyHttpClientFactory(ChatGptOptions options)
        {
            this.options = options;
        }

        public override HttpMessageHandler CreateMessageHandler()
        {
            if (options.Proxy.IsNullOrEmpty())
            {
                return new HttpClientHandler();
            }
            else
            {
                return new HttpClientHandler
                {
                    Proxy = new WebProxy(this.options.Proxy),
                    UseProxy = true
                };

            }
          
        }
    }
}
