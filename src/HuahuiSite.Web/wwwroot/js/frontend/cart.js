// #region On Load

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
    swal({
        title: 'Are you sure ?',
        text: "คุณต้องการลบข้อมูล ใช่หรือไม่?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            let $cartItemId = parseInt($(e).closest(".tr-data-row").find("[name='hid-cart-item-id']").val());

            $.ajax({
                type: "GET",
                url: "/Cart/DeleteCartItem",
                data: { cartItemId: $cartItemId },
                success: function (res) {
                    if (res.isSuccess) {
                        Swal('Deleted!', 'ลบข้อมูลสำเร็จ', 'success');
                        window.location = "/Cart/Index";
                    } else {
                        swal("Deleted Failed", "", "error");
                    }
                },
                error: function () { }
            });

            
        }
        });
};

/**
  * @desc Calculate Total Price of Unit Product
  * @param {Object} e - Element of Quantity Input
  * @param {Event} event - Event of Quantity Input
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
CalculateTotalPrice = (e) => {
    let inputQuantity = parseInt($(e).val());
    let maxQuantityOfProduct = parseInt($(e).prop("max"));

    // #region Check Input Quantity more than Max Quantity

    if (inputQuantity <= maxQuantityOfProduct) {
        let $trOfCartItem = $(e).closest("tr");

        let $totalPriceElement = $trOfCartItem.find(".product_total");

        let $productUnitPrice = parseInt($trOfCartItem.find(".product-price").text());
        let $productQuantity = parseInt($trOfCartItem.find("[name='ProductQuantity']").val());

        let $totalPrice = $productUnitPrice * $productQuantity;

        $totalPriceElement.text($totalPrice);

        UpdateCartItemList($trOfCartItem, $productQuantity, $totalPrice);

        UpdateTotalOfSummary();
    } else {
        // If Input Quantity more than Max Quantity to Remove last Character
        $(e).val(inputQuantity.toString().substr(0, inputQuantity.toString().length - 1));
    }

    // #endregion
};

/**
  * @desc Update Total Of Summary
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdateTotalOfSummary = () => {
    let $totalQuantity = 0;
    let $totalPrice = 0;

    $("#form-cart-item").find("#table-data").find(".tr-data-row").each(function (index, element) {
        $totalQuantity += parseInt($(element).find("[name='ProductQuantity']").val());
        $totalPrice += parseInt($(element).find(".product_total").text());
    });

    $("#td-summary-quantity-product").text($totalQuantity);
    $("#td-summary-total-product").text($totalPrice);
};

/**
  * @desc Update Cart Item List
  * @param {Object} trOfProduct - Element of tr of Product
  * @param {Number} productQuantity - Product Quantity
  * @param {Number} totalPrice - Total Price Of Product
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdateCartItemList = (trOfProduct, productQuantity, totalPrice) => {
    let $cartItemId = trOfProduct.find("[name='hid-cart-item-id']").val();

    for (var i = 0; i < _cartItemList.length; i++) {
        if (_cartItemList[i].id === parseInt($cartItemId)) {
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
                swal("Updated Failed", "", "error");
            }
        },
        error: function () { }
    });
};

/**
  * @desc Check Out Cart
  * @param {Object} e - Element of Check Out Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
CheckOutCart = (e) => {
    let $form = $(e).closest("#form-cart-item");
    let $cartId = $form.find("[name='hid-cart-id']").val();
    let $orderId = $form.find("[name='hid-order-id']").val();
    let $customerId = $form.find("[name='select-customer']").val();

    $.ajax({
        type: "GET",
        url: "/Cart/CheckOut",
        data: { cartId: $cartId, customerId: $customerId },
        success: function (res) {
            if (res.isSuccess) {
                Swal('ทำการยืนยันการสั่งซื้อเรียบร้อย !', `หมายเลขการสั่งซื้อ: ${ $orderId }`, 'success');
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