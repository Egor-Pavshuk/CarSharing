using System;

namespace DAL.Interface
{
    
    public class Rent
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public DateTime StartDate { get; set; }
        public bool InsuranceCase { get; set; }
        public DateTime EndDate { get; set; }
        public string CustomerEmail { get; set; }
        public float Cost { get; set; }

        public Rent(int id, int offerId, DateTime startDate, string customerEmail, bool insuranceCase = false)
        {
            Id = id;
            OfferId = offerId;
            StartDate = startDate;
            CustomerEmail = customerEmail;
            InsuranceCase = insuranceCase;
            EndDate = DateTime.MinValue;
            Cost = 0;
        }
    }
}
