using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLayer.ModelsBll;
using WebCarSharing.Models.Offers;

namespace WebCarSharing2.Models.Rents
{
    public class RentView
    {
        public int OfferId { get; set;}
        public bool InsuranceCase { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Cost { get; set; }
        public CustomerBll Customer { get; set; }
        
    }
}