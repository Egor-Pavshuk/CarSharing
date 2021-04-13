using System;

namespace CarSharing
{
    public class SportCar: Car 
    {
        public string Type { get; set; }
        public SportCar(int id, string model, int year) : base(id, model, year)
        {
            Type = "Sport";
        }

        public override int CountShareCost(int term)
        {
            if(term <= 0)
                throw new Exception("Term can`t be below or equal 0!");
            const int twoDaysCost = 300;
            const int aboveTwoDaysCost = 100;
            
            if(term > 2)
                return twoDaysCost + aboveTwoDaysCost * (term - 2);

            return twoDaysCost;
        }
    }
}
