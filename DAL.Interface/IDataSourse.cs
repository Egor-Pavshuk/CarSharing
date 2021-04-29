using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IDataSourse : IDisposable
    {
        Offer GetOfferById(int id);
        List<Offer> GetAllOffers();
        List<MinivanOffer> GetAllMinivanOffers();
        List<OutroadCarOffer> GetAllOutroadCarOffers();
        List<SportCarOffer> GetAllSportCarOffers();
        int CreateRent(Rent rent);

    }
}
