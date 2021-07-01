using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCarSharing2.Models.Customer
{
    public class CustomersView
    {
        public List<CustomerView> Customers { get; set; }
        public PagingInfoView PagingInfo { get; set; }
    }
}