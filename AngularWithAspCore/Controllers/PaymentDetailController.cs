using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularWithAspCore.Misc;
using AspCoreBl;
using AspCoreBl.Interfaces;
using AspCoreBl.Misc;
using AspCoreBl.Model;
using AspCoreBl.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularWithAspCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentDetailController : BaseController
    {
        private IPaymentDetailRepository _paymentDetailRepository;

        public PaymentDetailController(IPaymentDetailRepository PaymentDetailRepositoryy)
        {
            _paymentDetailRepository = PaymentDetailRepositoryy;
        }

        /// <summary>
        /// Post men call
        /// https://localhost:44330/api/PaymentDetail/getpaymentlist?q={PageNo:1}
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getpaymentlist")]
        public async Task<IActionResult> GetPayMentList(string q)
        {
            var query = JsonConvert.DeserializeObject<Query>(q, AppCommon.SerializerSettings);
            var res = await _paymentDetailRepository.ListAsync(query);
            return OKResult(res);
        }


        /// <summary>
        /// https://localhost:44330/api/PaymentDetail/GetPaymentDetail?CurrentPage=2&PageSize=12
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPaymentDetail")]
        public  DataSourceResult<PaymentDetailDTO> GetPaymentDetail(PagedResult obj)
        {
            var res =  _paymentDetailRepository.GetPaymentDetailList(obj);
            return res;
        }

        [HttpPost]
        [Route("postpaymentdetail")]
        public IActionResult PostPaymentDetail([FromBody]PaymentDetailDTO dto)
        {
            try
            {
                _paymentDetailRepository.Save(dto);
                //return OKResult("Data save Successfully");
                return OKResult(1, "Data save Successfully");
            }
            catch (Exception ex)
            {
                return OKResult(2, ex.Message.ToString());
            }
        }
    }
}
