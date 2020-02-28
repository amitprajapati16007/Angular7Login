using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspCoreBl.Utility
{
    public class Pager
    {
        public int CurrentPage { get;  set; }
        public int PageSize { get; set; }
        public string sortOrder { get; set; }
        public string sortColumnName { get; set; }
    }
}
