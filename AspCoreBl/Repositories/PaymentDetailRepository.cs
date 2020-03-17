using AspCoreBl.Interfaces;
using AspCoreBl.Model;
using AspCoreBl.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AspCoreBl.Misc;

namespace AspCoreBl.Repositories
{
    public class PaymentDetailRepository : GenericRepository<PaymentDetail>, IPaymentDetailRepository
    {
        private readonly PaymentDetailContext _db;

        public PaymentDetailRepository(PaymentDetailContext db) : base(db)
        {
            _db = db;
        }
        public async Task<DataSourceResult<PaymentDetailDTO>> ListAsync(Query q)
        {
            var query = from p in _db.PaymentDetail
                        select new PaymentDetailDTO()
                        {
                            PMID = p.PMID,
                            CardOwnerName = p.CardOwnerName,
                            CardNumber = p.CardNumber
                        };
            return await query.ToDatsSourceResultAsync(q);
        }
        
        public void Save(PaymentDetailDTO dto)
        {
            var obj = _db.PaymentDetail.FirstOrDefault(p => p.PMID == dto.PMID);
            if (obj == null)
            {
                obj = new PaymentDetail
                {
                    DateCreated = DateTime.Now,
                };                
                _db.PaymentDetail.Add(obj);
            }
            obj.CardNumber = dto.CardNumber;
            obj.CardOwnerName = dto.CardOwnerName;
            obj.expirationDate = dto.expirationDate;
            obj.CVV = dto.CVV;
            obj.DateUpdated = DateTime.Now;
            _db.SaveChanges();

        }
    }
}
