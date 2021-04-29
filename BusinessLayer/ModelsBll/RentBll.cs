using System;

namespace CarSharing
{
    
    public class RentBll
    {
        public int Id { get; set; }
        public OfferBll OfferBll { get; set; }
        public int Term { get; set; }
        public float Cost { get; set; }
        public string CustomerEmail { get; set; }
        private readonly DateTime _date;
        public bool InsuranceCase { get; set; }
        
        public RentBll(OfferBll offerBll, bool insuranceCase)
        {
            OfferBll = offerBll;
            _date = DateTime.Now;
            InsuranceCase = insuranceCase;
        }
        
        public int GetShareCost() => InsuranceCase ? OfferBll.CountShareCost(DateTime.Now.Day - _date.Day) + 150 : OfferBll.CountShareCost(DateTime.Now.Day - _date.Day);
        public void CreateInsuranceCase() => InsuranceCase = true;
    }
}
