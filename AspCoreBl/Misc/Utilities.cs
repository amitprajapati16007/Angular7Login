using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreBl.Misc
{
    public class Utilities
    {
        public Utilities()
        {

        }

        public static string GetCurrHost(IHttpContextAccessor httpContext)
        {
            if (httpContext != null)
            {
                var currHttpScheme = httpContext.HttpContext.Request.Scheme;
                var currHost = httpContext.HttpContext.Request.Host.Value;
                var currHostUrl = currHttpScheme + "://" + currHost;
                return currHostUrl;
            }
            else
            {
                return null;
            }
        }
    }
}
