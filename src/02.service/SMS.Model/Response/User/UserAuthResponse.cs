using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Response.User
{
    public class UserAuthResponse
    {
        public string Token { get; set; }
        public object Data { get; set; }
    }
}
