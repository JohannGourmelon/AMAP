﻿using AMAPG4.Models.Catalog;
using AMAPG4.Models.ContactForm;
using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class DashboardController : Microsoft.AspNetCore.Mvc.Controller
    {
      
        public IActionResult Index()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                dashboardVM.UserAccounts = userAccountDal.GetAllUserAccounts();
            }
            using (IndividualDal individualDal = new IndividualDal())
            {
                dashboardVM.Individuals = individualDal.GetAllIndividuals();
            }
            using (ProducerDal producerDal = new ProducerDal())
            {
                dashboardVM.Producers = producerDal.GetAllProducers();
            }
            using (CEDal ceDal = new CEDal())
            {
                dashboardVM.CEs = ceDal.GetAllCEs();
            }
            using (ProductDal productDal = new ProductDal())
            {
                dashboardVM.Products = productDal.GetAllProducts();
            }
            using (ContactService contactService = new ContactService())
            {
                dashboardVM.PendingContacts = contactService.GetAllPendingContacts();
            }
            using (NewProductService newProductService = new NewProductService())
            {
                dashboardVM.NewProducts = newProductService.GetAllPendingNewProducts();
            }

            return View(dashboardVM);

            
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Individuals()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (IndividualDal individualDal = new IndividualDal())
            {
                dashboardVM.Individuals = individualDal.GetAllIndividuals();
            }
            return View(dashboardVM);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Producers()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (ProducerDal producerDal = new ProducerDal())
            {
                dashboardVM.Producers = producerDal.GetAllProducers();
            }
            return View(dashboardVM);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult CEs()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (CEDal ceDal = new CEDal())
            {
                dashboardVM.CEs = ceDal.GetAllCEs();
            }
            return View(dashboardVM);
        }
        public IActionResult Contacts() {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (ContactService contactService = new ContactService())
            {
                dashboardVM.PendingContacts = contactService.GetAllPendingContacts();
                dashboardVM.DoneContacts = contactService.GetAllDoneContacts();
            }
            return View(dashboardVM);
        }
        public IActionResult Products()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (ProductDal productDal = new ProductDal())
            {
                dashboardVM.Products = productDal.GetAllProducts();
          
            }
            return View(dashboardVM);
        }
        public IActionResult NewProducts()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (NewProductService newProductService = new NewProductService())
            {
                dashboardVM.NewProducts = newProductService.GetAllPendingNewProducts();
                dashboardVM.RefusedProducts = newProductService.GetAllRefusedNewProducts();
            }
            return View(dashboardVM);
        }
    }


}
