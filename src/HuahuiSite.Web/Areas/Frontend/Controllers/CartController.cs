using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Frontend.Controllers
{
    [Area("Frontend")]
    public class CartController : Controller
    {

        #region Members

        private readonly ICartService _cartService;

        #endregion

        #region Constructor

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        #endregion

        #region Views

        public IActionResult Index()
        {
            CartViewModel cartViewModel = new CartViewModel();

            try
            {
                _cartService.GetCartItemList(ref cartViewModel);
            }
            catch (Exception exception)
            {

            }

            return View();
        }

        #endregion

        #region Actions

        public IActionResult AddToCart()
        {
            CartViewModel cartViewModel = new CartViewModel();

            try
            {
                _cartService.SaveCart(cartViewModel);

                RedirectToAction("Index");
            }
            catch (Exception exception)
            {

            }

            return View();
        }

        #endregion
    }
}