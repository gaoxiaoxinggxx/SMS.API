using System;

namespace SMS.Model.Response.Test
{
    public class CheckCronResponse
    {
        public bool Success { get; set; }

        public DateTime NextTime { get; set; }
    }
}