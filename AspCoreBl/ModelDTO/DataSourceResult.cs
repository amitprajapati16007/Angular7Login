using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreBl.ModelDTO
{
    public class DataSourceResult<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public object Aggregates { get; set; }
    }
}
