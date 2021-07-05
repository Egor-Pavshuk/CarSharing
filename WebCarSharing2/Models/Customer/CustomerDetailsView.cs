using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCarSharing.Models.Offers;
using WebCarSharing2.Models.Rents;


namespace WebCarSharing2.Models.Customer
{
    public class CustomerDetailsView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<RentView> Rents { get; set; }
        public List<OfferView> Offers { get; set; }
        public PagingInfoView PagingInfo { get; set; }
        public CustomerDetailsView()
        {
            Rents = new List<RentView>();
            Offers = new List<OfferView>();
        }

    }
}