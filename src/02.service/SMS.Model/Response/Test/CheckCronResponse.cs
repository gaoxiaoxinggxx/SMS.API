using System;

namespace BC.CSM.Response.Test
{
    public class CheckCronResponse
    {
        public bool Success { get; set; }

        public DateTime NextTime { get; set; }
    }
}