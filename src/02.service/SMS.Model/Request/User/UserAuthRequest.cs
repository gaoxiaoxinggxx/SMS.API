﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Request.User
{
    public class UserAuthRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
