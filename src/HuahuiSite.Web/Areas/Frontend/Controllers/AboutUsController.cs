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
    public class AboutUsController : Controller
    {
        #region Members

        private readonly IAboutUsService _aboutUsService;

        #endregion

        #region Constructor

        public AboutUsController
        (
            IAboutUsService aboutUsService
        )
        {
            _aboutUsService = aboutUsService;
        }

        #endregion

        #region Views

        public IActionResult Index()
        {
            MainViewModel mainViewModel = new MainViewModel();

            try
            {
                _aboutUsService.GetAboutUs(ref mainViewModel);
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