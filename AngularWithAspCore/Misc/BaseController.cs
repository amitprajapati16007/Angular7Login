using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspCoreBl.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public IActionResult OKResult<T>(string message, T data) where T : class
        {
            return Json(PrepareResultObject<object>(null, message, data), AppCommon.SerializerSettings);
        }

        public IActionResult OtherResult(HttpStatusCode code, string message)
        {
            var res = new JsonResult(PrepareResultObject(null, message, (object)null), AppCommon.SerializerSettings)
            {
                StatusCode = (int)code,
                ContentType = "application/json",
            };

            return res;
        }

        public IActionResult InvalidModelStateResult(ModelStateDictionary modelState)
        {
            var res = new JsonResult(PrepareResultObject(null, null, modelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList()), AppCommon.SerializerSettings)
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ContentType = "application/json",
            };

            return res;
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
