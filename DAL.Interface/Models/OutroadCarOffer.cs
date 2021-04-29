using System;

namespace DAL.Interface
{
    public class OutroadCarOffer: Offer
    {
        public OutroadCarOffer(int id, string model, int year, string type, string image, string description)
            : base(id, model, year, image, description, type)
        {
        }

    }
}
