using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using CarSharing;
using DAL;
using WebCarSharing.Models.Offers;
using WebCarSharing.Tools;
using WebCarSharing2.Models;
using WebCarSharing2.Models.Offers;
using WebCarSharing2.Models.Pager;

namespace WebCarSharing2.Controllers
{
    
    public class OpenedRentsController : Controller
    {
        private readonly IService _sharingService;
        public OpenedRentsController()
        {
            string connectionString = new ConnectingToDb().GetConnectionStringToFileLocalBd();
            _sharingService = new SharingService(new DataSource(connectionString));
        }

        public ActionResult OpenedRents()
        {
            ParametersBll parametersBll = new ParametersBll
            {
                Page = 1,
                PageSize = PagerParameters.PageSize
            };

            List<OfferView> offers = new List<OfferView>();

            foreach (var offer in _sharingService.GetTakenOffers(parametersBll))
            {
                const bool isTaken = true;

                offers.Add(new OfferView
                {
                    Id = offer.Id,
                    Model = offer.Model,
                    Description = offer.Description,
                    Year = offer.Year,
                    Image = offer.Image,
                    Type = offer.Type,
                    DailyCost = offer.DailyCost,
                    IsTaken = isTaken
                });
            }

            int totalItems = offers.Count;

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
        public ActionResult OpenedRents(ParametersView parameters)
        {
            ParametersBll parametersBll = new ParametersBll()
            {
                PriceFilter = parameters.PriceFilter,
                TypeFilter = parameters.TypeFilter,
                Page = parameters.Page,
                PageSize = PagerParameters.PageSize
            };
            List<OfferView> offers = new List<OfferView>();
            List<OfferBll> offersBll = _sharingService.GetSelectedTypes(parametersBll); //todo modify method for searching taken rents

            _sharingService.Sort(parametersBll, offersBll);
            offersBll = _sharingService.GetCurrentPageItems(offersBll, parametersBll); // todo may be error

            foreach (var offer in offersBll)
            {
                bool isTaken = _sharingService.IsOfferTaken(offer.Id);
                offers.Add(new OfferView
                {
                    Id = offer.Id,
                    Model = offer.Model,
                    Description = offer.Description,
                    Year = offer.Year,
                    Image = offer.Image,
                    Type = offer.Type,
                    DailyCost = offer.DailyCost,
                    IsTaken = isTaken
                });
            }

            List<OfferView> takenOffers = new List<OfferView>();
            foreach (OfferView offer in offers)
            {
                if(offer.IsTaken)
                    takenOffers.Add(offer);
            }

            int totalItems = takenOffers.Count;

            PagingInfoView pagingInfo = new PagingInfoView
            {
                CurrentPage = parameters.Page,
                ItemsPerPage = PagerParameters.PageSize,
                TotalItems = totalItems
            };

            OffersView offerView = new OffersView()
            {
                Offers = takenOffers,
                PagingInfo = pagingInfo
            };

            return View(offerView);
        }
    }

}