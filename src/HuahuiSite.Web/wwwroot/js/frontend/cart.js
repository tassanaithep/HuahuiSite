﻿// #region On Load

/**
  * @desc On Page Load
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
$(function () {
    
});

// #endregion

// #region Functions

/**
  * @desc Remove Cart Item from Cart
  * @param {Object} e - Element of Remove Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
RemoveCartItem = (e) => {
    let $cartItemId = parseInt($(e).closest(".tr-data-row").find("[name='hid-cart-item-id']").val());

    // #region Remove Cart Item from Cart Item List

    _cartItemList = _cartItemList.filter(function (cartItemList) {
        return cartItemList.id !== $cartItemId;
    });

    // #endregion

    $(e).closest(".tr-data-row").remove();
};

/**
  * @desc Calculate Total Price of Unit Product
  * @param {Object} e - Element of Quantity Input
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
CalculateTotalPrice = (e) => {
    let $trOfCartItem = $(e).closest("tr");

    let $totalPriceElement = $trOfCartItem.find(".product_total");

    let $productUnitPrice = parseInt($trOfCartItem.find(".product-price").text());
    let $productQuantity = parseInt($trOfCartItem.find("[name='ProductQuantity']").val());

    let $totalPrice = $productUnitPrice * $productQuantity;

    $totalPriceElement.text($totalPrice);

    UpdateCartItemList($trOfCartItem, $productQuantity, $totalPrice);
};

/**
  * @desc Update Cart Item List
  * @param {Object} trOfProduct - Element of tr of Product
  * @param {Number} productQuantity - Product Quantity
  * @param {Number} totalPrice - Total Price Of Product
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdateCartItemList = (trOfProduct, productQuantity, totalPrice) => {
    let cartItemId = trOfProduct.find("[name='hid-cart-item-id']").val();

    for (var i = 0; i < _cartItemList.length; i++) {
        if (_cartItemList[i].id === parseInt(cartItemId)) {
            _cartItemList[i].quantity = productQuantity;
            _cartItemList[i].totalPrice = totalPrice;
            break;
        }
    }
};

// #endregion

// #region Render

// #endregion

// #region Affect

// #endregion

// #region Action

/**
  * @desc Update Cart
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdateCart = () => {
    $.ajax({
        type: "POST",
        url: "/Cart/UpdateCart",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(_cartItemList),
        success: function (res) {
            if (res.isSuccess) {
                window.location = "/Cart/Index";
            } else {
                alert("Error");
            }
        },
        error: function () { }
    });
};

/**
  * @desc Update Cart
  * @param {Object} e - Element of Check Out Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
CheckOutCart = (e) => {
    let $cartId = $(e).closest("#form-cart-item").find("[name='hid-cart-id']").val();

    $.ajax({
        type: "GET",
        url: "/Cart/CheckOut",
        data: { cartId: $cartId },
        success: function (res) {
            if (res.isSuccess) {
                window.location = "/Cart/Index";
            } else {
                alert("Error");
            }
        },
        error: function () { }
    });
};

// #endregion

// #region Validate

// #endregion

// #region Response

// #endregion