using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspCoreBl.Model
{
    public class ApplicationRole : IdentityRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get => base.Id; set => base.Id = value; }
        
        [Column(TypeName = "nvarchar(100)")]
        public string DisplayName { get; set; }
    }
}
