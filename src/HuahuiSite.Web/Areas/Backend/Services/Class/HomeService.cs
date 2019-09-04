using AutoMapper;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
{
    public class HomeService : IHomeService
    {
        #region Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public HomeService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        #endregion

        #region Read

        /// <summary>
        /// Get Sales List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetCompleteOrderList(ref HomeViewModel homeViewModel)
        {
            #region Bind Order List Data

            homeViewModel.OrderList = _unitOfWork.Orders.GetOrderList();
            homeViewModel.OrderItemList = _unitOfWork.OrderItemLists.GetOrderItemList();
            homeViewModel.CompleteOrderItemList = _unitOfWork.OrderItemLists.GetCompleteOrderItemList();

            #endregion

            #region Bind StartDate and EndDate of Current Month

            homeViewModel.StartDate = DateTime.Today.AddDays(-(DateTime.Today.Day - 1)).ToString("MM/dd/yyyy");
            homeViewModel.EndDate = DateTime.Today.AddMonths(+1).AddDays(-(DateTime.Today.Day - 1)).AddDays(-1).ToString("MM/dd/yyyy");

            #endregion

            //#region Bind StartDate and EndDate of Last Month

            //homeViewModel.StartDate = DateTime.Today.AddMonths(-1).AddDays(-(DateTime.Today.Day - 1)).ToString("MM/dd/yyyy");
            //homeViewModel.EndDate = DateTime.Today.AddDays(-(DateTime.Today.Day - 1)).AddDays(-1).ToString("MM/dd/yyyy");

            //#endregion
        }

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion

        #endregion

        #region Actions

        public void Search(ref HomeViewModel homeViewModel)
        {
            homeViewModel.OrderList = _unitOfWork.Orders.GetOrderListOfSearch(homeViewModel.StartDate, homeViewModel.EndDate, homeViewModel.CustomerName, homeViewModel.SaleName);

            if (homeViewModel.OrderList.Count() > 0)
            {
                homeViewModel.OrderItemList = _unitOfWork.OrderItemLists.GetOrderItemList();
                homeViewModel.CompleteOrderItemList = _unitOfWork.OrderItemLists.GetCompleteOrderItemList();
            }
        }

        #endregion

        #region Functions

        public void ExportToFile(ref byte[] result, string orderId)
        {
            var orderList = _unitOfWork.OrderItemLists.GetOrderItemListDataByOrderId(orderId);

            #region Add Column Header

            var comlumHeadrsOrderId = new string[]
            {
                "Order Id:",
                orderId,
                "",
                "",
                "",
                ""
            };

            var comlumHeadrsCustomerName = new string[]
            {
                "Customer Name:",
                orderList.FirstOrDefault().CustomerName,
                "",
                "",
                "",
                ""
            };

            var comlumHeadrsCustomerAddress = new string[]
            {
                "Address:",
                orderList.FirstOrDefault().CustomerAddress,
                "",
                "",
                "",
                ""
            };

            var comlumHeadrsCustomerPhoneNumber = new string[]
            {
                "Phone Number:",
                orderList.FirstOrDefault().CustomerPhoneNumber,
                "",
                "",
                "",
                ""
            };

            var comlumHeadrsSaleName = new string[]
            {
                "Sale Name:",
                orderList.FirstOrDefault().SaleName,
                "",
                "",
                "",
                ""
            };

            var comlumHeadrsDate = new string[]
            {
                "Date:",
                DateTime.Now.ToString(),
                "",
                "",
                "",
                ""
            };

            var comlumHeadrs = new string[]
            {
                "No",
                "Product Id",
                "Product Name",
                "Unit Price",
                "Quantity",
                "Total Price"
            };

            #endregion

            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook

                var worksheet = package.Workbook.Worksheets.Add("Report-Order-"+orderId); //Worksheet name
               
                using (var cells = worksheet.Cells[1, 1, 1, 5]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrsOrderId.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = comlumHeadrsOrderId[i];
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrsCustomerName.Count(); i++)
                {
                    worksheet.Cells[2, i + 1].Value = comlumHeadrsCustomerName[i];
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrsCustomerAddress.Count(); i++)
                {
                    worksheet.Cells[3, i + 1].Value = comlumHeadrsCustomerAddress[i];
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrsCustomerPhoneNumber.Count(); i++)
                {
                    worksheet.Cells[4, i + 1].Value = comlumHeadrsCustomerPhoneNumber[i];
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrsSaleName.Count(); i++)
                {
                    worksheet.Cells[5, i + 1].Value = comlumHeadrsSaleName[i];
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrsDate.Count(); i++)
                {
                    worksheet.Cells[6, i + 1].Value = comlumHeadrsDate[i];
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrs.Count(); i++)
                {
                    worksheet.Cells[8, i + 1].Value = comlumHeadrs[i];
                    worksheet.Cells[8, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[8, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[8, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[8, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                }

                int indexOfCell = 9;
                int numberCount = 1;

                foreach (var item in orderList)
                {
                    worksheet.Cells["A" + indexOfCell].Value = numberCount++;
                    worksheet.Cells["B" + indexOfCell].Value = item.ProductId;
                    worksheet.Cells["C" + indexOfCell].Value = item.ProductName;
                    worksheet.Cells["D" + indexOfCell].Value = item.UnitPrice;
                    worksheet.Cells["E" + indexOfCell].Value = item.Quantity;
                    worksheet.Cells["F" + indexOfCell].Value = item.TotalPrice;


                    worksheet.Cells["A" + indexOfCell+":F"+ indexOfCell].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells["A" + indexOfCell + ":F" + indexOfCell].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells["A" + indexOfCell + ":F" + indexOfCell].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells["A" + indexOfCell + ":F" + indexOfCell].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    indexOfCell++;
                }
             
            
                result = package.GetAsByteArray();
            }
        }

        #endregion
    }
}
