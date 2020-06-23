using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspCoreBl.Model
{
   public abstract class BaseEntity
    {
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateUpdated { get; set; }
    }

    public class PagedResult
    {
        public PagedResult()
        {
            CurrentPage = 1;
            PageSize = 10;
        }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize  { get; set; }
        public int RowCount { get; set; }

    }
}
