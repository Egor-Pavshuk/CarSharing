using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ParametersBll
    {
        public string PriceFilter { get; set; }
        public string TypeFilter { get; set; }
        public int Page;
        public int PageSize;
    }
}
