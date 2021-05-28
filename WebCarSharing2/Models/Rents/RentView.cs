using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCarSharing.Models.Offers;

namespace WebCarSharing2.Models.Rents
{
    public class RentView
    {
        public int OfferId { get; set;}
        public string CustomerEmail { get; set; }
        public bool InsuranceCase { get; set; }

        
    }
}