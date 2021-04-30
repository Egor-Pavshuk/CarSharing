using System;

namespace CarSharing
{
    public class OutroadCarOfferBll: OfferBll
    {
        public OutroadCarOfferBll(int id, string model, int year, string image, string description)
            : base(id, model, year, image, description, "Outroad car")
        {
            DailyCost = 140;
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
