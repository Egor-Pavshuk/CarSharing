using System;

namespace DAL.Interface
{
    public class SportCarOffer: Offer 
    {
        public SportCarOffer(int id, string model, int year, string type, string image, string description)
            : base(id, model, year, image, description, type)
        {
        }

    }
}
