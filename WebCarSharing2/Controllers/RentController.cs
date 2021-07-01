using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.ModelsBll;
using CarSharing;
using DAL;
using WebCarSharing.Models.Offers;
using WebCarSharing.Tools;
using WebCarSharing2.Models.Rents;

namespace WebCarSharing2.Controllers
{
    public class RentController : Controller
    {
        private readonly IService _sharingService;
        public RentController()
        {
            string connectionString = new ConnectingToDb().GetConnectionStringToFileLocalBd();
            _sharingService = new SharingService(new DataSource(connectionString));
        }

        public ActionResult CreateRent(int offerIndex)
        {
            RentView rentView = new RentView {OfferId = offerIndex};
            CustomerBll customerBll = new CustomerBll();
            return View(rentView);
        }

        [HttpPost]
        public ActionResult CreateRent(RentView rentView)
        {
            CustomerBll customerBll = new CustomerBll
            {
                FirstName = rentView.Customer.FirstName,
                Surname = rentView.Customer.Surname,
                Email = rentView.Customer.Email
            };
            RentBll rentBll = new RentBll(_sharingService.GetOfferById(rentView.OfferId), rentView.Customer.Email, rentView.InsuranceCase)
            {
                Customer = customerBll
            };
            _sharingService.CreateRent(rentBll);
            return Redirect("/Home/Index");
        }

        public ActionResult CloseRent(int offerIndex)
        {
            OfferBll offerBll = _sharingService.GetOfferById(offerIndex);
            bool isTaken = _sharingService.IsOfferTaken(offerBll.Id);
            ViewBag.Offer = new OfferView
            {
                Id = offerBll.Id,
                Model = offerBll.Model,
                Description = offerBll.Description,
                Year = offerBll.Year,
                Image = offerBll.Image,
                Type = offerBll.Type,
                DailyCost = offerBll.DailyCost,
                IsTaken = isTaken
            };
            return View(new RentView {OfferId = offerIndex});
        }

        [HttpPost]
        public ActionResult CloseRent(RentView rentView)
        {
            RentParametersBll parameters = new RentParametersBll
                {CustomerEmail = rentView.Customer.Email, OfferId = rentView.OfferId};

            RentBll rentBll = _sharingService.GetOpenRentByOfferId(parameters);

            if (rentBll == null)
            {
               return RedirectToAction("CustomerNotFound");
            }
            
            int cost = rentBll.GetShareCost();
            _sharingService.CloseRent(rentBll);

            rentView.Cost = cost;
            rentView.EndDate = rentBll.EndDate;
            rentView.StartDate = rentBll.StartDate;
            rentView.InsuranceCase = rentBll.InsuranceCase;
            TempData["model"] = rentView;

            return RedirectToAction("SharingCost");
            
        }

        public ActionResult SharingCost()
        {
            return View(TempData["model"]);
        }

        public ActionResult CustomerNotFound()
        {
            return View();
        }
    }
}