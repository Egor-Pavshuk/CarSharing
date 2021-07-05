using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Models;

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
        int GetOffersCount();
        List<Offer> GetAllOffers(Parameters parameters);
        int GetMinivansCount();
        int GetOutroadCarsCount();
        int GetSportCarsCount();
        int CountOfNullEndDateByOfferId(int offerId);
        Rent GetOpenRentByOfferId(RentParameters parameters);
        int CloseRent(Rent rent);
        List<Offer> GetTakenOffers();
        List<Customer> GetAllCustomers();
        int AddNewCustomer(Customer customer);
        int IsCustomerExist(Customer customer);
        Customer GetCustomerById(int customerIndex);
        List<Rent> GetRentsByEmail(string email);
    }
}
