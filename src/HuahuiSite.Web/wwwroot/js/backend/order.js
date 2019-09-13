// #region On Load

/**
  * @desc On Page Load
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
$(function () {
    //RenderImage();
    //BindData();
    //$("#table-data").DataTable();
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
        let $productGroupCode = $(element).find("[name='hid-order-product-group-code']").val();
        let $quantity = $(element).find("[name='Quantity']").val();
        let $isPromotion = JSON.parse($(element).find("[name='hid-order-is-promotion']").val());

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
    let $trOfCartItem = $(e).closest("tr");

    let $totalPriceElement = $trOfCartItem.find("[name='TotalPrice']");

    let $productUnitPrice = parseInt($trOfCartItem.find("[name='hid-cart-item-unit-price']").val());
    let $productQuantity = parseInt($trOfCartItem.find("[name='Quantity']").val());

    let $totalPrice = $productUnitPrice * $productQuantity;

    $totalPriceElement.val($totalPrice);
};

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
        url: "/Backend/Order/UpdateTable",
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
  * @desc Enter to Search of Table
  * @param {Object} e - Element of Input Search
  * @param {Event} event - Event of Input Search
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
EnterSearch = (e, event) => {
    var code = (event.keyCode ? event.keyCode : event.which);

    if (code === 13) {
        let $textOfSearch = $(e).val();
        if ($textOfSearch != '') {
            window.location = `/Backend/Order/Index?keywordForSearch=${$textOfSearch}`;
        } else {
            swal("กรอกคำค้นหาด้วยครับ", "", "warning");
        }
    }
};

/**
  * @desc Delete Cart
  * @param {Object} e - Element of Delete Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
Delete = (e) => {
    let $cartId = $(e).closest(".tr-data-row").find(".form-row-table").find("[name='hid-cart-id']").val();

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
  * @desc Delete Cart Item of Cart
  * @param {Object} e - Element of Delete Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
DeleteCartItem = (e) => {
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
            // Remove Cart Item of Cart
            $(e).closest("tr").remove();

            // Alert Result of Delete
            Swal('Deleted!', 'Your file has been deleted.', 'success');
        }
    });
};

/**
  * @desc Complete Order
  * @param {Object} e - Element of Complete Order Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
CompleteOrder = (e) => {
    let $orderId = $(e).closest(".tr-data-row").find(".form-row-table").find("[name='hid-order-id']").val();

    swal({
        title: 'Are you sure ?',
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
                url: "/Backend/Order/Complete",
                data: { orderId: $orderId },
                success: function (res) {
                    if (res.isSuccess) {
                        //UpdatePage();
                        swal("Complete Success", "", "success");
                        window.location = "/Backend/Order/Index?isUpdate=true";
                    } else {
                        swal("Complete Failed", "", "error");
                    }
                },
                error: function () { }
            });
        }
    });
};

/**
  * @desc Cancel Order
  * @param {Object} e - Element of Cancel Order Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
CancelOrder = (e) => {
    let $orderId = $(e).closest(".tr-data-row").find(".form-row-table").find("[name='hid-order-id']").val();

    swal({
        title: 'Are you sure ?',
        text: "คุณต้องการยกเลิกการสั่งซื้อ ใช่หรือไม่ ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Confirm it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "GET",
                url: "/Backend/Order/Cancel",
                data: { orderId: $orderId },
                success: function (res) {
                    if (res.isSuccess) {
                        //UpdatePage();
                        swal("Delete Order Success", "", "success");
                        window.location = "/Backend/Order/Index?isUpdate=true";
                    } else {
                        swal("Cancel Failed", "", "error");
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