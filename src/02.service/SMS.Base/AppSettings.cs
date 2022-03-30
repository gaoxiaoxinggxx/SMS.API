using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Base
{
    public class AppSettings : IAppSettings
    {
        public WebApiProjectConfig WebApiProjectConfig { get; set; }
        public AppClientAuthConfig AppClientAuthConfig { get; set; }
        public SwaggerConfig SwaggerConfig { get; set; }
    }

    public class WebApiProjectConfig
    {
        public string Name { get; set; }
        public string CorsDefaultName { get; set; }
        public string Origins { get; set; }
        public bool IsDevelopment { get; set; }
    }

    public class AppClientAuthConfig
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public long DefaultExpiredMinutes { get; set; } = 30;
    }

    public class SwaggerConfig
    {
        public bool Enable { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}
