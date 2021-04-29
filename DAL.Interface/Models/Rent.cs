using System;

namespace DAL.Interface
{
    
    public class Rent
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public DateTime StartDate { get; set; }
        public bool InsuranceCase { get; set; }
        public int EndDate { get; set; }
        public string CustomerEmail { get; set; }
        public float Cost { get; set; }

        public Rent(int id, int offerId, string customerEmail, bool insuranceCase = false)
        {
            Id = id;
            OfferId = offerId;
            StartDate = DateTime.Now;
            CustomerEmail = customerEmail;
            InsuranceCase = insuranceCase;
        }
    }
}
