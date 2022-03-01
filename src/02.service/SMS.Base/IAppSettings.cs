using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Base
{
    public interface IAppSettings
    {
        WebApiProjectConfig WebApiProjectConfig { get; set; }
    }
}
