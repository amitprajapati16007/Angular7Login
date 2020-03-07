using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreBl.Model
{
    public class ErrorDetail
    {
        public Exception Ex { get; set; }
        public HttpRequest Request { get; set; }
        public string ConnectionId { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string Payload { get; set; }
        public string Userid { get; set; }
        public string UserEmail { get; set; }
        public string RemoteIp { get; set; }
        public DateTime DateTime { get; set; }
        public string TimezoneName { get; set; }
    }
}
