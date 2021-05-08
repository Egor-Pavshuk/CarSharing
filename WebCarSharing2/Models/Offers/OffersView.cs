using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCarSharing2.Models;

namespace WebCarSharing.Models.Offers
{
    public class OffersView
    {
        public List<OfferView> Offers { get; set; }
        public PagingInfoView PagingInfo { get; set; }
    }
}