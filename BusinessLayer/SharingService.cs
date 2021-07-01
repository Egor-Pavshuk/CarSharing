using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ModelsBll;
using BusinessLayer.Tools;
using CarSharing;
using DAL.Interface;
using DAL.Interface.Models;

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
            AddNewCustomer(rent.Customer);
            _dataSource.CreateRent(new Rent(rent.Id, rent.OfferBll.Id, rent.StartDate, rent.Customer.Email, rent.InsuranceCase));
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

        public int GetOffersCount(ParametersBll parameters)
        {
            switch (parameters.TypeFilter)
            {
                case "All types":
                    return _dataSource.GetOffersCount();
                case "Minivan":
                    return _dataSource.GetMinivansCount();
                case "Sport car":
                    return _dataSource.GetSportCarsCount();
                case "Out-road car":
                    return _dataSource.GetOutroadCarsCount();
                default:
                    throw new Exception("Incorrect type");
            }
        }

        public List<OfferBll> GetAllOffers(ParametersBll parametersBll)
        {
            List<OfferBll> offers = new List<OfferBll>();

            foreach (Offer offer in _dataSource.GetAllOffers())
            {
                offers.Add(FactoryClass.CreateChild(offer));
            }
            return offers.GetRange((parametersBll.Page - 1) * parametersBll.PageSize,parametersBll.PageSize);
        }

        public List<OfferBll> GetSelectedTypes(ParametersBll parameters)
        {
            List<OfferBll> offerBll = new List<OfferBll>();

            switch (parameters.TypeFilter)
            {
                case "All types":
                    return GetAllOffers();
                case "Minivan":
                    foreach (var offer in GetAllMinivanOffers())
                    {
                        offerBll.Add(offer);
                    }
                    break;
                case "Sport car":
                    foreach (var offer in GetAllSportCarOffers())
                    {
                        offerBll.Add(offer);
                    }
                    break;
                case "Out-road car":
                    foreach (var offer in GetAllOutroadCarOffersOffers())
                    {
                        offerBll.Add(offer);
                    }
                    break;
                default:
                    throw new Exception("Incorrect type");
            }

            return offerBll;
        }

        public List<OfferBll> Sort(ParametersBll parameters, List<OfferBll> offers) 
        {
            switch (parameters.PriceFilter)
            {
                case "Up":
                    return SortPriceUp(offers);
                case "Down":
                    return SortPriceDown(offers);
                case "Sort":
                    return offers;
                default:
                    throw new Exception("Incorrect filter!");
            }
        }

        private List<OfferBll> SortPriceUp(List<OfferBll> offers)
        {
            for (int i = 1; i < offers.Count; i++)
            {
                for (int j = 0; j < offers.Count - i; j++)
                {
                    if (offers[j].DailyCost > offers[j + 1].DailyCost)
                    {
                        var temp = offers[j];
                        offers[j] = offers[j + 1];
                        offers[j + 1] = temp;
                    }
                }
            }

            return offers;
        }

        private List<OfferBll> SortPriceDown(List<OfferBll> offers)
        {
            for (int i = 1; i < offers.Count; i++)
            {
                for (int j = 0; j < offers.Count - i; j++)
                {
                    if (offers[j].DailyCost < offers[j + 1].DailyCost)
                    {
                        var temp = offers[j];
                        offers[j] = offers[j + 1];
                        offers[j + 1] = temp;
                    }
                }
            }

            return offers;
        }

        public List<OfferBll> GetCurrentPageItems(List<OfferBll>offers, ParametersBll parameters)=>  
            offers.Skip((parameters.Page - 1) * parameters.PageSize).Count() >= parameters.PageSize ?
                offers.GetRange((parameters.Page - 1) * parameters.PageSize, parameters.PageSize):
                offers.Skip((parameters.Page - 1) * parameters.PageSize).ToList();

        public bool IsOfferTaken(int offerIndex)
        {
            if (_dataSource.CountOfNullEndDateByOfferId(offerIndex) == 0)
                return false;
            return true;
        }

        public List<OfferBll> GetTakenOffers(ParametersBll parametersBll)
        {
            List<OfferBll> offersBll = new List<OfferBll>();
            foreach (Offer offer in _dataSource.GetTakenOffers())
            {
                offersBll.Add(FactoryClass.CreateChild(offer));
            }

            if (offersBll.Count < parametersBll.PageSize)
                return offersBll;
            return offersBll.GetRange((parametersBll.Page - 1) * parametersBll.PageSize, parametersBll.PageSize);
        }

        #region Rent

        public RentBll GetOpenRentByOfferId(RentParametersBll parameters)
        {
            Rent rent = _dataSource.GetOpenRentByOfferId(new RentParameters
            {
                CustomerEmail = parameters.CustomerEmail,
                OfferId = parameters.OfferId
            });

            if (rent == null)
                return null;

            OfferBll offerBll = FactoryClass.CreateChild(_dataSource.GetOfferById(parameters.OfferId));

            return new RentBll(offerBll, rent.CustomerEmail, rent.InsuranceCase)
            {
                StartDate = rent.StartDate,
                Id = rent.Id
            };
        }

        public void CloseRent(RentBll rent)
        {
            rent.Cost = rent.GetShareCost();
            rent.EndDate = DateTime.Now;
            _dataSource.CloseRent(
                new Rent(rent.Id, rent.OfferBll.Id, rent.StartDate, rent.Customer.Email, rent.InsuranceCase)
                {
                    Cost = rent.Cost,
                    EndDate = rent.EndDate
                });
        }

        #endregion

        public List<CustomerBll> GetAllCustomers()
        {
            List<CustomerBll> customersBll = new List<CustomerBll>();

            foreach (var customer in _dataSource.GetAllCustomers())
            {
                customersBll.Add(new CustomerBll()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    Surname = customer.Surname,
                    Email = customer.Email
                });
            }

            return customersBll;
        }

        public void AddNewCustomer(CustomerBll customerBll)
        {
            Customer customer = new Customer
            {
                Id = customerBll.Id,
                FirstName = customerBll.FirstName,
                Surname = customerBll.Surname,
                Email = customerBll.Email
            };

            if(_dataSource.IsCustomerExist(customer) != 1)
                _dataSource.AddNewCustomer(customer);
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
