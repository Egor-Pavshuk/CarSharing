using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarSharing;
using DAL.Interface;

namespace BusinessLayer.Tools
{
    public sealed class FactoryClass
    {
        public static OfferBll CreateChild(Offer offer)
        {
            switch (offer.Type)
            {
                case "Minivan":
                    return new MinivanOfferBll(offer.Id, offer.Model, offer.Year, offer.Image, offer.Description);
                case "SportCar":
                    return new SportCarOfferBll(offer.Id, offer.Model, offer.Year, offer.Image, offer.Description);
                case "OutroadCar":
                    return new OutroadCarOfferBll(offer.Id, offer.Model, offer.Year, offer.Image, offer.Description);
                default:
                    throw new Exception("Incorrect type of car!");
            }
        }
    }
}
