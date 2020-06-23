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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularWithAspCore.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    public class PaymentDetailController : BaseController
    {
        private IPaymentDetailRepository _paymentDetailRepository;

        public PaymentDetailController(IPaymentDetailRepository PaymentDetailRepositoryy)
        {
            _paymentDetailRepository = PaymentDetailRepositoryy;
        }

       
        [HttpGet]
        [Route("getpaymentlist")]
        public async Task<IActionResult> GetPayMentList(string q)
        {
            var query = JsonConvert.DeserializeObject<Query>(q, AppCommon.SerializerSettings);
            var res = await _paymentDetailRepository.ListAsync(query);
            return OKResult(res);
        }

        [HttpGet]
        [Route("getpaymentlist")]
        public  DataSourceResult<PaymentDetailDTO> GetPaymentDetailList(PagedResult obj)
        {
            var res =  _paymentDetailRepository.GetPaymentDetailList(obj);
            return res;
        }

        [HttpPost]
        [Route("postpaymentdetail")]
        public void PostPaymentDetail([FromBody]PaymentDetailDTO dto)
        {
            try
            {
                _paymentDetailRepository.Save(dto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
