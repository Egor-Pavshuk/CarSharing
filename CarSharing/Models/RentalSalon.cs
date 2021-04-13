using System;
using System.Collections.Generic;

namespace CarSharing
{
    class RentalSalon
    {
        private List<Rent> _rents;
        private string _name;
        private string _customerSurname;

        public RentalSalon(string surname, string name, List<Rent> rents)
        {
           _customerSurname = surname;
           _name = name;
           _rents = rents;
        }

        public void AddRent(Rent rent) => _rents.Add(rent);

        public void GetReport()
        {
            foreach (var rent in _rents)
            {
                rent.GetInformation();
                Console.WriteLine();
            }
        }

        public int GetFullCost()
        {
            int cost = 0;
            foreach (var rent in _rents)
            {
                cost += rent.GetShareCost();
            }

            return cost;
        }
    }
}
