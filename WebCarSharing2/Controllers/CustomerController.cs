using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessLayer;
using BusinessLayer.ModelsBll;
using CarSharing;
using DAL;
using WebCarSharing.Models.Offers;
using WebCarSharing.Tools;
using WebCarSharing2.Models;
using WebCarSharing2.Models.Customer;
using WebCarSharing2.Models.Offers;
using WebCarSharing2.Models.Pager;
using WebCarSharing2.Models.Rents;

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

        [HttpPost]
        public ActionResult AllCustomers(ParametersView parameters)
        {
            ParametersBll parametersBll = new ParametersBll
            {
                Page = parameters.Page,
                PageSize = PagerParameters.PageSize
            };


            List<CustomerBll> customersBll = _sharingService.GetAllCustomers();
            int totalItems = customersBll.Count;
            customersBll = _sharingService.GetCurrentPageCustomers(customersBll, parametersBll);

            List<CustomerView> customersView = new List<CustomerView>();
            foreach (var customer in customersBll)
            {
                customersView.Add(new CustomerView
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    Surname = customer.Surname
                });
            }

            PagingInfoView pagingInfo = new PagingInfoView
            {
                CurrentPage = parameters.Page,
                ItemsPerPage = PagerParameters.PageSize,
                TotalItems = totalItems
            };

            CustomersView customers = new CustomersView
            {
                Customers = customersView,
                PagingInfo = pagingInfo
            };

            return View(customers);
        }

        public ActionResult CustomerDetails(int customerIndex)
        {
            ParametersBll parametersBll = new ParametersBll
            {
                PageSize = PagerParameters.PageSize,
                Page = 1
            };

            CustomerBll customerBll = _sharingService.GetCustomerById(customerIndex);

            CustomerDetailsView customerDetails = new CustomerDetailsView
            {
                Id = customerBll.Id,
                FirstName = customerBll.FirstName,
                Surname = customerBll.Surname,
                Email = customerBll.Email
            };

            foreach (RentBll rent in _sharingService.GetRentsByEmail(customerBll.Email))
            {
                customerDetails.Rents.Add(new RentView
                {
                    OfferId = rent.OfferBll.Id,
                    Cost = rent.Cost,
                    Customer = customerBll,
                    StartDate = rent.StartDate,
                    EndDate = rent.EndDate,
                    InsuranceCase = rent.InsuranceCase
                });
            }

            foreach (var rent in customerDetails.Rents)
            {
                OfferBll offerBll = _sharingService.GetOfferById(rent.OfferId);
                OfferView offerView = new OfferView
                {
                    Id = offerBll.Id,
                    Model = offerBll.Model,
                    Description = offerBll.Description,
                    Year = offerBll.Year,
                    Image = offerBll.Image,
                    Type = offerBll.Type,
                    DailyCost = offerBll.DailyCost,
                    IsTaken = _sharingService.IsOfferTaken(offerBll.Id)
                };
                
                customerDetails.Offers.Add(offerView);
            }

            int totalItems = customerDetails.Offers.Count;
            PagingInfoView pagingInfo = new PagingInfoView
            {
                CurrentPage = 1,
                ItemsPerPage = PagerParameters.PageSize,
                TotalItems = totalItems
            };

            if (customerDetails.Offers.Count > PagerParameters.PageSize)
            {
                customerDetails.Offers = customerDetails.Offers.GetRange(0, PagerParameters.PageSize);
                customerDetails.Rents = customerDetails.Rents.GetRange(0, PagerParameters.PageSize);
            }
                
            
            customerDetails.PagingInfo = pagingInfo; 
            return View(customerDetails);
        }

        [HttpPost]
        public ActionResult CustomerDetails(ParametersView parameters)
        {
            ParametersBll parametersBll = new ParametersBll
            {
                PageSize = PagerParameters.PageSize,
                Page = parameters.Page
            };

            CustomerBll customerBll = _sharingService.GetCustomerById(parameters.CustomerId);

            CustomerDetailsView customerDetails = new CustomerDetailsView
            {
                Id = customerBll.Id,
                FirstName = customerBll.FirstName,
                Surname = customerBll.Surname,
                Email = customerBll.Email
            };

            foreach (RentBll rent in _sharingService.GetRentsByEmail(customerBll.Email))
            {
                customerDetails.Rents.Add(new RentView
                {
                    OfferId = rent.OfferBll.Id,
                    Cost = rent.Cost,
                    Customer = customerBll,
                    StartDate = rent.StartDate,
                    EndDate = rent.EndDate,
                    InsuranceCase = rent.InsuranceCase
                });
            }

            foreach (var rent in customerDetails.Rents)
            {
                OfferBll offerBll = _sharingService.GetOfferById(rent.OfferId);
                OfferView offerView = new OfferView
                {
                    Id = offerBll.Id,
                    Model = offerBll.Model,
                    Description = offerBll.Description,
                    Year = offerBll.Year,
                    Image = offerBll.Image,
                    Type = offerBll.Type,
                    DailyCost = offerBll.DailyCost,
                    IsTaken = _sharingService.IsOfferTaken(offerBll.Id)
                };

                customerDetails.Offers.Add(offerView);
            }

            int totalItems = customerDetails.Offers.Count;
            PagingInfoView pagingInfo = new PagingInfoView
            {
                CurrentPage = parameters.Page,
                ItemsPerPage = PagerParameters.PageSize,
                TotalItems = totalItems
            };

            if (customerDetails.Offers.Skip((parameters.Page - 1) * PagerParameters.PageSize).Count() > PagerParameters.PageSize)
            {
                customerDetails.Offers = customerDetails.Offers.GetRange((parameters.Page - 1) * PagerParameters.PageSize, PagerParameters.PageSize);
                customerDetails.Rents = customerDetails.Rents.GetRange((parameters.Page - 1) * PagerParameters.PageSize, PagerParameters.PageSize);
            }
            else
            {
                customerDetails.Offers =
                    customerDetails.Offers.Skip((parameters.Page - 1) * PagerParameters.PageSize).ToList();
                customerDetails.Rents =
                    customerDetails.Rents.Skip((parameters.Page - 1) * PagerParameters.PageSize).ToList();
            }

            customerDetails.PagingInfo = pagingInfo;

            return View(customerDetails);
        }
    }
}