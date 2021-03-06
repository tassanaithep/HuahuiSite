﻿// #region On Load

/**
  * @desc On Page Load
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
$(function () {
    BindProductPriceByQuantity();
});

// #endregion

// #region Renders

BindProductPriceByQuantity = () => {
    $(".tr-data-item-row").each(function (index, element) {
        let $productGroupCode = $(element).find("[name='ProductGroupCode']").val();
        let $quantity = $(element).find("[name='Quantity']").val();
        let $isPromotion = JSON.parse($(element).find("[name='IsPromotion']").val());

        $.ajax({
            type: "GET",
            url: "/Backend/Home/GetProductPriceByQuantity",
            data: { productGroupCode: $productGroupCode, quantity: $quantity },
            success: function (res) {
                let productGroupModel = res;

                if (productGroupModel !== null) {
                    let unitPrice = productGroupModel.unitPrice;
                    let promotionPrice = productGroupModel.promotionPrice;

                    if (!$isPromotion) {
                        $(element).find("[name='UnitPrice']").val(unitPrice);
                    }
                    else {
                        $(element).find("[name='UnitPrice']").val(promotionPrice);
                    }
                }
            },
            error: function () { }
        });
    });
};

// #endregion

// #region Functions

// #endregion

// #region Affects

// #endregion

// #region Actions

/**
  * @desc Export Order to File
  * @param {String} orderId - Order Id of Order
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ExportToFile = (orderId) => {
    window.location = `/Backend/Home/ExportToFile?orderId=${ orderId }`;
};

// #endregion

// #region Validates

// #endregion

// #region Responses

// #endregion