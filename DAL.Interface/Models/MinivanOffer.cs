using System;

namespace DAL.Interface
{
    public class MinivanOffer: Offer
    {
        public MinivanOffer(int id, string model, int year, string type, string image, string description) 
            : base(id, model, year, image, description, type)
        {
        }
    }
}
