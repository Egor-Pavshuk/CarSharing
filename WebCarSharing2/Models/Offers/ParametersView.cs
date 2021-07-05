using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCarSharing2.Models.Offers
{
    public class ParametersView
    {
        public int Page { get; set; }
        public string PriceFilter { get; set; }
        public string TypeFilter { get; set; }
        public int CustomerId { get; set; }
        public ParametersView()
        {
            Page = 1;
        }
    }
}