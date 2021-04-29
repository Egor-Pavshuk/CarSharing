using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Tools;
using CarSharing;
using DAL.Interface;

namespace BusinessLayer
{
    public class SharingService : IDisposable, IService
    {
        private readonly IDataSourse _dataSource;

        public SharingService(IDataSourse dataSource)
        {
            _dataSource = dataSource;
        }

        public void CreateRent(RentBll rent)
        {
            _dataSource.CreateRent(new Rent(rent.Id, rent.OfferBll.Id, rent.CustomerEmail, rent.InsuranceCase));
        }
        
        public List<MinivanOfferBll> GetAllMinivanOffers()
        {
            List<MinivanOfferBll> minivanOfferBll= new List<MinivanOfferBll>();
            foreach (var item in _dataSource.GetAllMinivanOffers())
            {
                minivanOfferBll.Add(new MinivanOfferBll(item.Id, item.Model, item.Year, item.Image, item.Description));
            }

            return minivanOfferBll;
        }

        public List<OfferBll> GetAllOffers()
        {
            List<OfferBll> offersBll = new List<OfferBll>();
            foreach (var offer in _dataSource.GetAllOffers())
            {
                offersBll.Add(FactoryClass.CreateChild(offer));
            }

            return offersBll;
        }

        public List<OutroadCarOfferBll> GetAllOutroadCarOffersOffers()
        {
            List<OutroadCarOfferBll> outroadCarOfferBll = new List<OutroadCarOfferBll>();
            foreach (var item in _dataSource.GetAllOutroadCarOffers())
            {
                outroadCarOfferBll.Add(new OutroadCarOfferBll(item.Id, item.Model, item.Year, item.Image, item.Description));
            }

            return outroadCarOfferBll;
        }

        public List<SportCarOfferBll> GetAllSportCarOffers()
        {
            List<SportCarOfferBll> sportCarOfferBll = new List<SportCarOfferBll>();
            foreach (var item in _dataSource.GetAllSportCarOffers())
            {
                sportCarOfferBll.Add(new SportCarOfferBll(item.Id, item.Model, item.Year, item.Image, item.Description));
            }

            return sportCarOfferBll;
        }

        public OfferBll GetOfferById(int id)
        {
            Offer offer = _dataSource.GetOfferById(id);
            return FactoryClass.CreateChild(offer);
        }

        

        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                _dataSource.Dispose();
                _disposed = true;
            }
        }
    }
}
