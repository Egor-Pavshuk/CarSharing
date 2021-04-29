using System;

namespace CarSharing
{
    public class MinivanOfferBll: OfferBll
    {
        public MinivanOfferBll(int id, string model, int year, string image, string description)
            : base(id, model, year, image, description, "Minivan")
        {
           // DailyCost = 30;
        }

        public override int CountShareCost(int term) => term <=0 ? throw new Exception("Term can`t be below or equal 0!") : 30 * term;

    }
}
