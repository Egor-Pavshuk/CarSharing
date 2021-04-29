using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarSharing;

namespace BusinessLayer
{
    public interface IService
    {
        void CreateRent(RentBll rent);
        List<MinivanOfferBll> GetAllMinivanOffers();
        List<OfferBll> GetAllOffers();
        List<OutroadCarOfferBll> GetAllOutroadCarOffersOffers();
        List<SportCarOfferBll> GetAllSportCarOffers();
        OfferBll GetOfferById(int id);


    }
}
