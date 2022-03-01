using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commen.Helper
{
    public static class IsAjaxRequestExtensions
    {
        /// <summary>
        /// 判断当前是否ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
