// #region On Load

/**
  * @desc On Page Load
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
$(function () {
    //RenderImage();
    //BindData();
    $("#table-data").DataTable();
    BindProductPriceByQuantity();
});

// #endregion

// #region Renders

/**
  * @desc Render Image of Datatable
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
RenderImage = () => {
    $(".form-row-table").each(function (index, element) {
        let $titlePictureFileName = $(element).find("[name='TitlePictureFileName']").val();

        if ($titlePictureFileName !== "") {
            $(element).find(".dropify-wrapper").addClass("has-preview");
            $(element).find(".dropify-preview").css("display", "block");
            $(element).find(".dropify-render").append(`<img src="/images/upload/${$titlePictureFileName}" />`);
        }
    });
};

/**
  * @desc Bind Data
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
BindData = () => {
    // #region Select

    $(".form-row-table").each(function (index, element) {
        // #region Get Element

        let $saleSelect = $(element).find("[name='SaleId']");

        // #endregion

        // #region Get Value

        let $saleId = $(element).find("[name='hid-sale-id']").val();

        // #endregion

        // #region Binding Value

        $saleSelect.val($saleId);

        // #endregion
    });

    // #endregion

    // #region Column of Table

    $(".form-row-table").each(function (index, element) {
        // #region Get Element

        let $tdOfSaleName = $(element).closest("tr").find(".td-sale-name");

        // #endregion

        // #region Get Value

        let $saleName = $(element).find("[name='SaleId'] :selected").text();

        // #endregion

        // #region Binding Value

        $tdOfSaleName.text($saleName);

        // #endregion
    });

    // #endregion
};

/**
  * @desc Bind Product Price By Quantity
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
BindProductPriceByQuantity = () => {
    $("#table-data").find(".tr-data-item-row").each(function (index, element) {
        let $productGroupCode = $(element).find("[name='hid-cart-product-group-code']").val();
        let $quantity = $(element).find("[name='Quantity']").val();
        let $isPromotion = JSON.parse($(element).find("[name='hid-cart-is-promotion']").val());

        $.ajax({
            type: "GET",
            url: "/Home/GetProductPriceByQuantity",
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

/**
  * @desc Remove Cart Item from Cart
  * @param {Object} e - Element of Remove Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
RemoveCartItem = (e) => {
    let $cartItemId = $(e).closest("tr").find("[name='hid-cart-item-id']").val();

    // #region Remove Cart Item from Cart Item List

    for (var i = 0; i < _cartItemList.length; i++) {
        if (_cartItemList[i].id === parseInt($cartItemId)) {
            delete _cartItemList[i];
            _cartItemList.splice(i, i);
            break;
        }
    }

    // #endregion

    $(e).closest("tr").remove();
};

/**
  * @desc Calculate Total Price of Unit Product
  * @param {Object} e - Element of Quantity Input
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
CalculateTotalPrice = (e) => {
    let $trOfCartItem = $(e).closest(".tr-data-item-row");

    let $productGroupCode = $trOfCartItem.find("[name='hid-cart-product-group-code']").val();
    let $quantity = $trOfCartItem.find("[name='Quantity']").val();
    let $isPromotion = JSON.parse($trOfCartItem.find("[name='hid-cart-is-promotion']").val());

    $.ajax({
        type: "GET",
        async: false,
        url: "/Backend/Home/GetProductPriceByQuantity",
        data: { productGroupCode: $productGroupCode, quantity: $quantity },
        success: function (res) {
            let productGroupModel = res;

            let unitPrice = productGroupModel.unitPrice;
            let promotionPrice = productGroupModel.promotionPrice;

            if (!$isPromotion)
            {
                $trOfCartItem.find("[name='UnitPrice']").val(unitPrice);
            }
            else
            {
                $trOfCartItem.find("[name='UnitPrice']").val(promotionPrice);
            }
        },
        error: function () { }
    });

    let $totalPriceElement = $trOfCartItem.find("[name='TotalPrice']");

    let $productUnitPrice = parseInt($trOfCartItem.find("[name='UnitPrice']").val());
    let $productQuantity = parseInt($trOfCartItem.find("[name='Quantity']").val());

    let $totalPrice = $productUnitPrice * $productQuantity;

    $totalPriceElement.val($totalPrice);

    UpdateTotalOfSummary(e);
};

/**
  * @desc Update Total Of Summary
  * @param {Object} e - Element of Quantity Input
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdateTotalOfSummary = (e) => {
    let $totalQuantity = 0;
    let $totalPrice = 0;

    $(e).closest(".table-data-item").find(".tr-data-item-row").each(function (index, element) {
        $totalQuantity += parseInt($(element).find("[name='Quantity']").val());
        $totalPrice += parseInt($(element).find("[name='TotalPrice']").val());
    });

    $(e).closest(".table-data-item").find("[name='summary-quantity']").val($totalQuantity);
    $(e).closest(".table-data-item").find("[name='summary-total-price']").val($totalPrice);
};

/**
  * @desc Change Quantity of Cart Item
  * @param {Object} e - Element of Input Quantity
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
//ChangeQuantityOfCartItem = (e) => {
//    $(e).closest(".tr-data-row").find("[name='hid-cart-item-quantity']").val($(e).val());
//};

// #endregion

// #region Affects

/**
  * @desc Update Page
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdatePage = () => {
    HideModal();
    //ClearForm();
    UpdateTable();
    //DropifyScriptRender();
    //RenderImage();
    //BindData();
    $("#table-data").DataTable();
    $("body").removeAttr("style");
};

/**
  * @desc Clear Create Form
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ClearForm = () => {
    $("#form-create")[0].reset();
    $("#form-create").find(".dropify-wrapper").remove();
    $("#parent-form-group-title-picture").append(`<input class="dropify" type="file" id="TitlePictureImageFile" name="TitlePictureImageFile" />`);
};

/**
  * @desc Hide Modal
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
HideModal = () => {
    $(".modal").modal("hide");
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
};

/**
  * @desc Update Table of Data
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdateTable = () => {
    $.ajax({
        url: "/Backend/Cart/UpdateTable",
        async: false,
        success: function (res) {
            $("#parent-table").html(res);
        },
        error: function () { }
    });
};

// #endregion

// #region Actions

/**
  * @desc Delete Cart
  * @param {Object} e - Element of Delete Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
Delete = (e) => {
    let $cartId = $(e).closest(".tr-data-row").find(".form-row-table").find("[name='hid-cart-id']").val();

    swal({
        title: 'Are you sure ?',
        text: "คุณต้องการลบข้อมูล ใช่หรือไม่ ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "GET",
                url: "/Backend/Cart/Delete",
                data: { cartId: $cartId },
                success: function (res) {
                    if (res.isSuccess) {
                        UpdatePage();
                        swal("Delete Success", "", "success");
                    } else {
                        swal("Delete Failed", "", "error");
                    }
                },
                error: function () { }
            });
        }
    });
};

/**
  * @desc Remove Cart Item of Cart
  * @param {Object} e - Element of Delete Button
  * @param {Event} event - Event of Delete Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
RemoveCartItem = (e, event) => {
    event.preventDefault();

    // Count remaining of Cart Item
    let countCartItem = 0;

    // Loop for Count Cart Item
    $(e).closest(".form-row-table").find(".tr-data-item-row").each(function (index, element) {
        countCartItem++;
    });

    // If Cart Item remaining one Item to Remove Total
    if (countCartItem === 1) {
        $(e).closest(".form-row-table").find(".tr-data-total-row").remove();
    }

    // Remove Cart Item of Cart
    $(e).closest("tr").remove();
};

/**
  * @desc Update Cart
  * @param {Object} e - Element of Submit Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdateCart = (e) => {
    let $cartId = $(e).closest(".form-row-table").find("[name='hid-cart-id']").val();

    let cartItemList = [];

    $(e).closest(".form-row-table").find(".tr-data-item-row").each(function (index, element) {
        // #region Get Value from Data Row

        let $id = parseInt($(element).find("[name='hid-cart-item-id']").val());
        let $cartId = parseInt($(element).find("[name='hid-cart-item-cart-id']").val());
        let $productId = parseInt($(element).find("[name='hid-cart-item-product-id']").val());
        let $quantity = parseInt($(element).find("[name='Quantity']").val());
        let $totalPrice = parseInt($(element).find("[name='TotalPrice']").val());
        let $isActive = $(element).find("[name='hid-cart-item-is-active']").val() === "true" ? true : false;
        let $createdDateTime = $(element).find("[name='hid-cart-item-created-datetime']").val();

        // #endregion

        // Add Value to Cart Item List
        cartItemList.push({ id: $id, cardId: $cartId, productId: $productId, quantity: $quantity, totalPrice: $totalPrice, isActive: $isActive, createdDateTime: $createdDateTime, updatedDateTime: null });
    });

    if (cartItemList.length === 0)
    {
        $.ajax({
            type: "GET",
            url: "/Backend/Cart/Update",
            data: { cartId: $cartId },
            success: function (res) {
                if (res.isSuccess) {
                    UpdatePage();
                    swal("Update Success", "", "success");
                } else {
                    swal("Update Success", "", "error");
                }
            },
            error: function () { }
        });
    }
    else
    {
        $.ajax({
            type: "POST",
            url: "/Backend/Cart/Update",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(cartItemList),
            success: function (res) {
                if (res.isSuccess) {
                    UpdatePage();
                    swal("Update Success", "", "success");
                } else {
                    swal("Update Success", "", "error");
                }
            },
            error: function () { }
        });
    }
};

/**
  * @desc Approve Cart
  * @param {Object} e - Element of Submit Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ApproveCart = (e) => {
    let $cartId = $(e).closest(".tr-data-row").find(".form-row-table").find("[name='hid-cart-id']").val();

    swal({
        title: 'Are you sure?',
        text: "คุณต้องการอนุมัติการสั่งซื้อ ใช่หรือไม่?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Confirm it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "GET",
                url: "/Backend/Cart/Approve",
                data: { cartId: $cartId },
                success: function (res) {
                    if (res.isSuccess) {
                        UpdatePage();
                        swal("Approve Success", "", "success");
                    } else {
                        swal("Approve Failed", "", "error");
                    }
                },
                error: function () { }
            });
        }
    });
};

// #endregion

// #region Validates

/**
  * @desc Validate before Update Submit
  * @param {Object} e - Element of Submit Button
  * @return {bool} Result of Submit
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ValidateUpdateSubmit = (e) => {
    let $form = $(e).closest("tr").find(".form-row-table");

    if ($form.find(".dropify-preview").css("display") === "none") {
        $form.find("[name='IsRemoveImage']").val(true);
    }
    else {
        $form.find("[name='IsRemoveImage']").val(false);
    }

    return true;
};

// #endregion

// #region Responses

/**
  * @desc On Form Action Save Success
  * @param {Object} res - Json of Result
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
OnSaveSuccess = (res) => {
    if (res.isSuccess) {
        UpdatePage();
        swal("Save Success", "", "success");
    } else {
        swal("Save Failed", "", "error");
    }
};

/**
  * @desc On Form Action Update Success
  * @param {Object} res - Json of Result
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
OnUpdateSuccess = (res) => {
    if (res.isSuccess) {
        UpdatePage();
        swal("Update Success", "", "success");
    } else {
        swal("Update Success", "", "error");
    }
};

// #endregion