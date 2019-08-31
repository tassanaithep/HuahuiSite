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
            #region Bind Order List and Cart List to Model

            #region Order

            homeViewModel.OrderList = _unitOfWork.Orders.GetOrderList();
            homeViewModel.OrderItemList = _unitOfWork.OrderItemLists.GetOrderItemList();

            #endregion

            #region Cart

            homeViewModel.CartList = _unitOfWork.Carts.GetConfirmCartList();
            homeViewModel.CartItemList = _unitOfWork.CartItemLists.GetCartItemListOfConfirmCart();

            #endregion

            #endregion

            #region Bind StartDate and EndDate of Current Month

            homeViewModel.StartDate = DateTime.Today.AddDays(-(DateTime.Today.Day - 1)).ToString("MM/dd/yyyy");
            homeViewModel.EndDate = DateTime.Today.AddMonths(+1).AddDays(-(DateTime.Today.Day - 1)).AddDays(-1).ToString("MM/dd/yyyy");

            #endregion
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

        public void GetProductPriceByQuantity(ref ProductGroupViewModel productGroup, string productGroupCode, int quantity)
        {
            productGroup = Mapper.Map<ProductGroup, ProductGroupViewModel>(_unitOfWork.ProductGroups.GetProductGroupByCodeAndQuantity(productGroupCode, quantity));
        }

        #endregion

        #region Functions

        public void ExportToFile(ref byte[] result, string orderId)
        {
            var orderList = _unitOfWork.OrderItemLists.GetOrderItemListDataByOrderId(orderId);

            #region Add Column Header

            //var comlumHeadrsOrderId = new string[]
            //{
            //    "Order Id:",
            //    orderId,
            //    "",
            //    "",
            //    "",
            //    ""
            //};

            var comlumHeadrsCustomerName = new string[]
            {
                "ชื่อลูกค้า :",
                orderList.FirstOrDefault().CustomerName,
                "",
                "วันที่ "+DateTime.Now.ToShortDateString(),
                "เวลาออกใบสั่งซื้อ "+DateTime.Now.ToShortTimeString(),
                "เซลล์ "+orderList.FirstOrDefault().SaleName,
                ""
            };

            var comlumHeadrsCustomerAddress = new string[]
            {
                "ที่อยู่จัดส่ง :",
                orderList.FirstOrDefault().CustomerAddress,
                "",
                "จ่าย ",
                "",
                "",
                ""
            };

            var comlumHeadrsCustomerPhoneNumber = new string[]
            {
                "เบอร์โทร :",
                orderList.FirstOrDefault().CustomerPhoneNumber,
                "",
                "VAT ",
                "NO VAT",
                "",
                ""
            };
            
            var comlumHeadrs = new string[]
            {
                "ที่",
                "รหัสสินค้า",
                "ชื่อ-รายการสินค้า",
                "ราคาต่อหน่วย",
                "จำนวน",
                "ราคารวม",
                "หมายเหตุ"
            };

            #endregion

            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook

                var worksheet = package.Workbook.Worksheets.Add("Report-Order-"+orderId); //Worksheet name

                //using (var cells = worksheet.Cells[1, 1, 1, 5]) //(1,1) (1,5)
                //{
                //    cells.Style.Font.Bold = true;
                //}

                //First add the headers
                //for (var i = 0; i < comlumHeadrsOrderId.Count(); i++)
                //{
                //    worksheet.Cells[1, i + 1].Value = comlumHeadrsOrderId[i];
                //}

                //First add the headers
                for (var i = 0; i < comlumHeadrsCustomerName.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = comlumHeadrsCustomerName[i];
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrsCustomerAddress.Count(); i++)
                {
                    worksheet.Cells[2, i + 1].Value = comlumHeadrsCustomerAddress[i];
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrsCustomerPhoneNumber.Count(); i++)
                {
                    worksheet.Cells[3, i + 1].Value = comlumHeadrsCustomerPhoneNumber[i];
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrs.Count(); i++)
                {
                    worksheet.Cells[5, i + 1].Value = comlumHeadrs[i];
                }

                int indexOfCell = 6;
                int numberCount = 1;

                #region Add Borders to Table

                worksheet.Cells["A" + 5 + ":G" + 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 5 + ":G" + 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 5 + ":G" + 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 5 + ":G" + 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                #endregion

                foreach (var item in orderList)
                {
                    worksheet.Cells["A" + indexOfCell].Value = numberCount++;
                    worksheet.Cells["B" + indexOfCell].Value = item.ProductId;
                    worksheet.Cells["C" + indexOfCell].Value = item.ProductName;
                    worksheet.Cells["D" + indexOfCell].Value = item.UnitPrice;
                    worksheet.Cells["E" + indexOfCell].Value = item.Quantity;
                    worksheet.Cells["F" + indexOfCell].Value = item.TotalPrice;
                    worksheet.Cells["G" + indexOfCell].Value = "";

                    #region Add Borders to Table

                    worksheet.Cells["A" + indexOfCell + ":G" + indexOfCell].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells["A" + indexOfCell + ":G" + indexOfCell].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells["A" + indexOfCell + ":G" + indexOfCell].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells["A" + indexOfCell + ":G" + indexOfCell].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    #endregion

                    indexOfCell++;
                }

                var BottomRow1 = new string[]
                {
                    "เวลาออกใบสั่งซื้อ "+DateTime.Now.ToShortTimeString(),
                    "",
                    "",
                    "จัดสินค้าโดย ",
                    "",
                    "",
                    ""
                };

                var BottomRow2 = new string[]
                {
                    "ออกบิลขายโดย ",
                    "",
                    "",
                    "เวลาจัดของเสร็จ ",
                    "",
                    "",
                    ""
                };

                indexOfCell++;

                //First add the headers
                for (var i = 0; i < BottomRow1.Count(); i++)
                {
                    worksheet.Cells[indexOfCell, i + 1].Value = BottomRow1[i];
                }

                indexOfCell++;

                //First add the headers
                for (var i = 0; i < BottomRow2.Count(); i++)
                {
                    worksheet.Cells[indexOfCell, i + 1].Value = BottomRow2[i];
                }

                result = package.GetAsByteArray();
            }
        }

        #endregion
    }
}
