using System;

namespace CarSharing
{
    
    public class RentBll
    {
        public int Id { get; set; }
        public OfferBll OfferBll { get; set; }
        public float Cost { get; set; }
        public string CustomerEmail { get; set; }
        public  DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool InsuranceCase { get; set; }
        
        public RentBll(OfferBll offerBll, string customerEmail, bool insuranceCase)
        {
            OfferBll = offerBll;
            StartDate = DateTime.Now.Subtract(new TimeSpan(3,0,0,0));
            CustomerEmail = customerEmail;
            InsuranceCase = insuranceCase;
        }
        
        public int GetShareCost() => InsuranceCase ? OfferBll.CountShareCost(DateTime.Now.Day - StartDate.Day) + 150 : OfferBll.CountShareCost(DateTime.Now.Day - StartDate.Day);
        public void CreateInsuranceCase() => InsuranceCase = true;
    }
}
