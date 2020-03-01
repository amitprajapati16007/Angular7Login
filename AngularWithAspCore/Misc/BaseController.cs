using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreBl.Misc;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularWithAspCore.Misc
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        public IActionResult OKResult(int status, string message)
        {
            return Json(PrepareResultObject(status, message, (object)null), AppCommon.SerializerSettings);
        }
        public IActionResult OKResult<T>(int status, string message, T data) where T : class
        {
            return Json(PrepareResultObject(status, message, data), AppCommon.SerializerSettings);
        }
        private ApiResult<T> PrepareResultObject<T>(int? status, string message, T data) where T : class
        {
            var resObj = new ApiResult<T>()
            {
                Data = data,
                Message = message,
                Status = status
            };

            return resObj;
        }
    }

    public class ApiResult<T> where T : class
    {
        public int? Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
