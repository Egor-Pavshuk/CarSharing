using System;

namespace DAL
{
    
    class Rent
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime Date { get; set; }
        public bool ?InsuranceCase { get; set; }
        public int Term { get; set; }
        public string CustomerEmail { get; set; }

        public Rent(int id, int carId, int term, string customerEmail, bool ?insuranceCase)
        {
            Id = id;
            CarId = carId;
            Date = DateTime.Now;
            Term = term;
            CustomerEmail = customerEmail;
            InsuranceCase = insuranceCase;
        }
    }
}
