//using AutoMapper;
//using HuahuiSite.Core.Entities;
//using HuahuiSite.Core.Interfaces;
//using HuahuiSite.Core.Models;
//using HuahuiSite.Web.Areas.Backend.Models;
//using HuahuiSite.Web.Areas.Backend.Services.Interface;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HuahuiSite.Web.Areas.Backend.Services.Class
//{
//    public class OrderService : IOrderService
//    {
//        #region Members

//        private readonly IUnitOfWork _unitOfWork;

//        #endregion

//        #region Constructor

//        public OrderService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        #endregion

//        #region CRUD

//        #region Create

//        /// <summary>
//        /// Save Order.
//        /// </summary>
//        // Author: Mod Nattasit
//        // Updated: 07/07/2019
//        public int SaveOrder(OrderViewModel customerViewModel)
//        {
//            #region Create Object to Save

//            Order customer = new Order()
//            {
//                Firstname = customerViewModel.Firstname,
//                Lastname = customerViewModel.Lastname,
//                Address = customerViewModel.PhoneNumber,
//                PhoneNumber = customerViewModel.PhoneNumber,
//                Email = customerViewModel.Email,
//                SaleId = customerViewModel.SaleId
//            };

//            #endregion

//            _unitOfWork.Orders.Add(customer);

//            return customer.Id;
//        }

//        #endregion

//        #region Read

//        /// <summary>
//        /// Get Order List.
//        /// </summary>
//        // Author: Mod Nattasit
//        // Updated: 07/07/2019
//        public void GetOrderList(ref OrderViewModel customerViewModel)
//        {
//            customerViewModel.OrderList = Mapper.Map<IEnumerable<OrderModel>, IEnumerable<OrderViewModel>>(_unitOfWork.Orders.GetOrderList());
//        }

//        #endregion

//        //#region Update

//        ///// <summary>
//        ///// Update Order.
//        ///// </summary>
//        //// Author: Mod Nattasit
//        //// Updated: 07/07/2019
//        //public void UpdateOrder(OrderViewModel customerViewModel)
//        //{
//        //    #region Create Object to Update

//        //    Order customer = new Order()
//        //    {
//        //        Id = customerViewModel.Id,
//        //        Firstname = customerViewModel.Firstname,
//        //        Lastname = customerViewModel.Lastname,
//        //        Address = customerViewModel.Address,
//        //        PhoneNumber = customerViewModel.PhoneNumber,
//        //        Email = customerViewModel.Email,
//        //        SaleId = customerViewModel.SaleId
//        //    };

//        //    #endregion

//        //    _unitOfWork.Orders.Update(customer);
//        //}

//        //#endregion

//        //#region Delete

//        ///// <summary>
//        ///// Delete Order.
//        ///// </summary>
//        //// Author: Mod Nattasit
//        //// Updated: 07/07/2019
//        //public void DeleteOrder(OrderViewModel customerViewModel)
//        //{
//        //    #region Create Object to Delete

//        //    Order customer = new Order()
//        //    {
//        //        Id = customerViewModel.Id,
//        //    };

//        //    #endregion

//        //    _unitOfWork.Orders.Remove(customer);
//        //}

//        //#endregion

//        #endregion
//    }
//}
