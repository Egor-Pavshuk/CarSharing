using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using CarSharing;
using DAL;
using DAL.Interface.Models;
using WebCarSharing.Models.Offers;
using WebCarSharing.Tools;
using WebCarSharing2.Models;
using WebCarSharing2.Models.Offers;
using WebCarSharing2.Models.Pager;

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
            ParametersBll parametersBll = new ParametersBll
            {
                Page = 1,
                PageSize = PagerParameters.PageSize
            };

            List<OfferView> offers = new List<OfferView>();

            foreach (var offer in _sharingService.GetAllOffers(parametersBll))
            {
                offers.Add(new OfferView
                {
                    Id = offer.Id, Model = offer.Model, Description = offer.Description, Year = offer.Year, Image = offer.Image,
                    Type = offer.Type, DailyCost = offer.DailyCost
                });
            }


            int totalItems = _sharingService.GetOffersCount(parametersBll);

            PagingInfoView pagingInfo = new PagingInfoView
            {
                CurrentPage = 1,
                ItemsPerPage = PagerParameters.PageSize,
                TotalItems = totalItems
            };
            
            OffersView offersView = new OffersView()
            {
                Offers = offers,
                PagingInfo = pagingInfo
            };

            return View(offersView);
        }

        [HttpPost]
        public ActionResult Index(ParametersView parameters)
        {
            ParametersBll parametersBll = new ParametersBll()
            {
                PriceFilter = parameters.PriceFilter, 
                TypeFilter = parameters.TypeFilter,
                Page = parameters.Page,
                PageSize = PagerParameters.PageSize
            };
            List<OfferView> offers = new List<OfferView>();
            List<OfferBll> offersBll = _sharingService.GetSelectedTypes(parametersBll);

            _sharingService.Sort(parametersBll, offersBll);
            offersBll = _sharingService.GetCurrentPageItems(offersBll, parametersBll);

            foreach (var offer in offersBll)
            {
                offers.Add(new OfferView
                {
                    Id = offer.Id,
                    Model = offer.Model,
                    Description = offer.Description,
                    Year = offer.Year,
                    Image = offer.Image,
                    Type = offer.Type,
                    DailyCost = offer.DailyCost
                });

                
            }

            int totalItems = _sharingService.GetOffersCount(parametersBll);

            PagingInfoView pagingInfo = new PagingInfoView
            {
                CurrentPage = parameters.Page,
                ItemsPerPage = PagerParameters.PageSize,
                TotalItems = totalItems
            };
            
            OffersView offerView = new OffersView()
            {
                Offers = offers,
                PagingInfo = pagingInfo
            };

            return View(offerView);
        }
        
    }
}