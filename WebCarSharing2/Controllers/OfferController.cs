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

namespace WebCarSharing2.Controllers
{
    public class OfferController : Controller
    {
        private readonly IService _sharingService;
        public OfferController()
        {
            string connectionString = new ConnectingToDb().GetConnectionStringToFileLocalBd();
            _sharingService = new SharingService(new DataSource(connectionString));
        }
        public ActionResult ShowOfferById(int offerIndex)
        {
            OfferBll offerBll = _sharingService.GetOfferById(offerIndex);
            OfferView offerView = new OfferView()
            {
                DailyCost = offerBll.DailyCost,
                Model = offerBll.Model,
                Description = offerBll.Description,
                Id = offerBll.Id,
                Image = offerBll.Image,
                Type = offerBll.Type,
                Year = offerBll.Year
            };

            return View(offerView);
        }
    }
}