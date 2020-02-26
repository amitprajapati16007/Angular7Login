using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreBl.ModelDTO
{
    public class PaymentDetailDTO
    {   
        public int PMID { get; set; }        
        public string CardOwnerName { get; set; }        
        public string CardNumber { get; set; }        
        public string expirationDate { get; set; }        
        public string CVV { get; set; }
    }
}
