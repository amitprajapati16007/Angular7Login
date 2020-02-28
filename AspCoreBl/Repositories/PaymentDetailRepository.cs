using AspCoreBl.Interfaces;
using AspCoreBl.Model;
using AspCoreBl.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AspCoreBl.Utility;

namespace AspCoreBl.Repositories
{
    public class PaymentDetailRepository : GenericRepository<PaymentDetail>, IPaymentDetailRepository
    {
        private readonly PaymentDetailContext _db;

        public PaymentDetailRepository(PaymentDetailContext db) : base(db)
        {
            _db = db;
        }
     
        public List<PaymentDetailDTO> GetList(Pager p)
        {
            List<PaymentDetailDTO> list=new List<PaymentDetailDTO>();
            var objPaymentDetail = (from s in _db.PaymentDetail
                                    select new PaymentDetailDTO()
                                    {
                                        PMID = s.PMID,
                                        CardOwnerName = s.CardOwnerName,
                                        CardNumber = s.CardNumber
                                    }
                            ).AsQueryable();
            switch (p.sortOrder)
            {
                case "asc":                    
                    switch (p.sortColumnName)
                    {
                        case "CardOwnerName":
                            objPaymentDetail = objPaymentDetail.OrderBy(s => s.CardOwnerName);
                            break;
                        case "CardNumber":
                            objPaymentDetail = objPaymentDetail.OrderBy(s => s.CardNumber);
                            break;
                    }
                    break;
                case "desc":
                    switch (p.sortColumnName)
                    {
                        case "CardOwnerName":
                            objPaymentDetail = objPaymentDetail.OrderByDescending(s => s.CardOwnerName);
                            break;
                        case "CardNumber":
                            objPaymentDetail = objPaymentDetail.OrderByDescending(s => s.CardNumber);
                            break;
                    }
                    break;
            }
           
            return list;
        }
        public void Save(PaymentDetailDTO dto)
        {
            var obj = _db.PaymentDetail.FirstOrDefault(p => p.PMID == dto.PMID);
            if (obj == null)
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
