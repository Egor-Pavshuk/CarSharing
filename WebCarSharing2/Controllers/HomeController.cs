using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DAL;
using WebCarSharing.Models.Offers;
using WebCarSharing.Tools;

namespace WebCarSharing2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService _sharingService;
        public HomeController()
        {
            string connectionString = new ConnectingToDb().GetConnectionStringToFileLocalBd();
            _sharingService = new SharingService(new DataSource(connectionString));
        }
        public ActionResult Index()
        {
            List<OfferView> offers = new List<OfferView>();
            foreach (var offer in _sharingService.GetAllOffers())
            {
                offers.Add(new OfferView
                {
                    Id = offer.Id, Model = offer.Model, Description = offer.Description, Year = offer.Year, Image = offer.Image,
                    Type = offer.Type
                });
            }

            return View(new OffersView{Offers = offers});
        }
        
    }
}