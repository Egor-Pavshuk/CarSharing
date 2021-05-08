﻿using System;
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
        List<OfferBll> GetSelectedTypes(ParametersBll parameters);
        List<OfferBll> Sort(ParametersBll parameters, List<OfferBll> offers);
        int GetOffersCount();
        List<OfferBll> GetAllOffers(ParametersBll parametersBll);

    }
}