using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspCoreBl.Model
{
    public class PaymentDetail
    {
        [Key]
        public int PMID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string CardOwnerName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(16)")]
        public string CardNumber { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(5)")]
        public string expirationDate { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(3)")]
        public string CVV { get; set; }
    }
}
