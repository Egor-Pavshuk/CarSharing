using System;

namespace CarSharing
{
    public class OutroadCar: Car
    {
        public string Type { get; set; }
        public OutroadCar(int id, string model, int year) : base(id, model, year)
        {
            Type = "Out-road";
        }

        public override int CountShareCost(int term)
        {
            if (term <= 0)
                throw new Exception("Term can`t be below or equal 0!");

            int twoDaysCost = 140, aboveTwoDaysCost = 50;

            if (term > 2)
                return twoDaysCost + aboveTwoDaysCost * (term - 2);

            return twoDaysCost;
        }
    }
}
