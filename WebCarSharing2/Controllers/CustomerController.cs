using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessLayer;
using DAL;
using WebCarSharing.Tools;
using WebCarSharing2.Models;
using WebCarSharing2.Models.Customer;
using WebCarSharing2.Models.Pager;

namespace WebCarSharing2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IService _sharingService;
        public CustomerController()
        {
            string connectionString = new ConnectingToDb().GetConnectionStringToFileLocalBd();
            _sharingService = new SharingService(new DataSource(connectionString));
        }
        public ActionResult AllCustomers()
        {
            List<CustomerView> customers = new List<CustomerView>();
            foreach (var customer in _sharingService.GetAllCustomers())
            {
                customers.Add(new CustomerView
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    Surname = customer.Surname
                });
            }

            PagingInfoView pagingInfo = new PagingInfoView
            {
                CurrentPage = 1,
                ItemsPerPage = PagerParameters.PageSize,
                TotalItems = customers.Count
            };

            CustomersView customersView = new CustomersView
            {
                Customers = customers,
                PagingInfo = pagingInfo
            };

            return View(customersView);
        }
    }
}