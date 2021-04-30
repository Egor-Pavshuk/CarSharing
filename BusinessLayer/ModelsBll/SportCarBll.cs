using System;

namespace CarSharing
{
    public class SportCarOfferBll: OfferBll 
    {
        public SportCarOfferBll(int id, string model, int year, string image, string description)
            : base(id, model, year, image, description, "Sport car")
        {
            DailyCost = 300;
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
