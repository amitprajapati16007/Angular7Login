using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreBl.Interfaces;
using AspCoreBl.Model;
using AspCoreBl.ModelDTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularWithAspCore.Controllers
{
    [Route("api/[controller]")]
    public class PaymentDetailController : Controller
    {
        private IPaymentDetailRepository _paymentDetailRepository;

        public PaymentDetailController(IPaymentDetailRepository PaymentDetailRepositoryy)
        {
            _paymentDetailRepository = PaymentDetailRepositoryy;
        }

        [HttpPost]
        [Route("PostPaymentDetail")]
        public void PostPaymentDetail(PaymentDetailDTO dto)
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
