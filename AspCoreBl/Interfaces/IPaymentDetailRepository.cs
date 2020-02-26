using AspCoreBl.Model;
using AspCoreBl.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreBl.Interfaces
{
    public interface IPaymentDetailRepository : IGenericRepository<PaymentDetail>
    {
        void Save(PaymentDetailDTO dto); 
    }
}
