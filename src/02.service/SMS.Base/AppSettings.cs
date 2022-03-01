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
    }

    public class WebApiProjectConfig
    {
        public string Name { get; set; }
        public string CorsDefaultName { get; set; }
        public string Origins { get; set; }
        public bool IsDevelopment { get; set; }
    }
}
