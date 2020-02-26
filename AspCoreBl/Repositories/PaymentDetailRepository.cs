using AspCoreBl.Interfaces;
using AspCoreBl.Model;
using AspCoreBl.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AspCoreBl.Repositories
{
    public class PaymentDetailRepository: GenericRepository<PaymentDetail>, IPaymentDetailRepository
    {
        private readonly PaymentDetailContext _db;

        public PaymentDetailRepository(PaymentDetailContext db) : base(db)
        {
            _db = db;
        }

        public void Save(PaymentDetailDTO dto) {
            var obj = _db.PaymentDetail.FirstOrDefault(p => p.PMID == dto.PMID);
            if (obj==null)
            {
                obj = new PaymentDetail
                {
                 
                };
                _db.PaymentDetail.Add(obj);
            }
            obj.CardNumber = dto.CardNumber;
            _db.SaveChanges();

        }
    }
}
