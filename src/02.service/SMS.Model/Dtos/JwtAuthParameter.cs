using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Dtos
{
    public class JwtAuthParameter
    {
        public Guid Key { get; set; }

        public string Secret { get; set; }
    }
}
