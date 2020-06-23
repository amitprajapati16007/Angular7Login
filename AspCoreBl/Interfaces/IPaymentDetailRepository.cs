using AspCoreBl.Model;
using AspCoreBl.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreBl.Interfaces
{
    public interface IPaymentDetailRepository : IGenericRepository<PaymentDetail>
    {
        Task<DataSourceResult<PaymentDetailDTO>> ListAsync(Query q);
        DataSourceResult<PaymentDetailDTO> GetPaymentDetailList(PagedResult obj);
        void Save(PaymentDetailDTO dto); 
    }
}
