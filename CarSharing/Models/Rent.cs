using System;

namespace CarSharing
{
    
    class Rent
    {
        private readonly Car _car;
        private readonly DateTime _date;
        private bool _insuranceCase;

        public Rent(Car car, int? term)
        {
            _car = car;
            _date = DateTime.Now;
        }

        public void GetInformation()
        {
           // _car.GetInformation();
            Console.WriteLine($"Share cost: {GetShareCost()}\nTerm: {DateTime.Now.Day - _date.Day}");
        }

        public int GetShareCost() => _insuranceCase ? _car.CountShareCost(DateTime.Now.Day - _date.Day) + 150 : _car.CountShareCost(DateTime.Now.Day - _date.Day);
        public void CreateInsuranceCase() => _insuranceCase = true;
    }
}
