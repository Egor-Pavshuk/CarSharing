using System;
using BusinessLayer.ModelsBll;

namespace CarSharing
{
    
    public class RentBll
    {
        public int Id { get; set; }
        public OfferBll OfferBll { get; set; }
        public float Cost { get; set; }
        public CustomerBll Customer { get; set; }
        public  DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool InsuranceCase { get; set; }

        
        public RentBll(OfferBll offerBll, string customerEmail, bool insuranceCase)
        {
            Customer = new CustomerBll();
            OfferBll = offerBll;
            StartDate = DateTime.Now.Subtract(new TimeSpan(3,0,0,0));
            Customer.Email = customerEmail;
            InsuranceCase = insuranceCase;
        }
        
        public int GetShareCost() => InsuranceCase ? OfferBll.CountShareCost(DateTime.Now.Subtract(StartDate).Days) + 150 : OfferBll.CountShareCost(DateTime.Now.Subtract(StartDate).Days);
        public void CreateInsuranceCase() => InsuranceCase = true;
    }
}
