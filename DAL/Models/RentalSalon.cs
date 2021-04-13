using System;
using System.Collections.Generic;

namespace DAL
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

        
    }
}
