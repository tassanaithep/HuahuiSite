using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Frontend.Controllers
{
    [Area("Frontend")]
    public class ContactUsController : Controller
    {

        #region Members

        private readonly IContactUsService _contactUsService;

        #endregion

        #region Constructor

        public ContactUsController
        (
            IContactUsService contactUsService
        )
        {
            _contactUsService = contactUsService;
        }

        #endregion

        #region Views

        public IActionResult Index()
        {
            MainViewModel mainViewModel = new MainViewModel();

            try
            {
                _contactUsService.GetContactUs(ref mainViewModel);
            }
            catch (Exception exception)
            {

                throw;
            }

            return View(mainViewModel);
        }

        #endregion
    }
}