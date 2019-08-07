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
  * @desc Open Modal of Product
  * @param {Object} e - Element of Open Modal Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
OpenProductModal = (e) => {
    // #region Get Value from Form

    let $form = $(e).closest(".form-product-item");

    let $formId = $form.find("[name='hid-form-product-id']").val();
    let $productId = $form.find("[name='ProductId']").val();
    let $name = $form.find("[name='ProductName']").val();
    let $unitPrice = $form.find("[name='ProductUnitPrice']").val();
    let $pictureFileName = $form.find("[name='ProductPictureFileName']").val();

    let $quantityOfItem = $form.find("[name='QuantityOfItem']");

    // #endregion

    // #region Get Quantity of Product

    let quantity = null;

    if (_cartItemList !== null) {
        for (var i = 0; i < _cartItemList.length; i++) {
            if (_cartItemList[i].productId === parseInt($productId)) {
                quantity = _cartItemList[i].quantity;
                break;
            }
        }
    }

    if (quantity === null) {
        $quantityOfItem.val(1);
    } else {
        $quantityOfItem.val(quantity);
    }

    // #endregion

    // #region Add Modal Element

    $("body").append(`
        <div class="modal fade" id="modal-product" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="ClearProductModal()">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="modal_body">
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-5 col-md-5 col-sm-12">
                                    <div class="modal_tab">
                                        <div class="tab-content product-details-large">
                                            <div class="tab-pane fade show active" id="tab1" role="tabpanel">
                                                <div class="modal_tab_img">
                                                    <a href="#"><img src="/images/upload/${ $pictureFileName }" alt=""></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-7 col-md-7 col-sm-12">
                                    <div class="modal_right">
                                        <div class="modal_title mb-10">
                                            <h2>${ $name }</h2>
                                        </div>
                                        <div class="modal_price mb-10">
                                            <span class="new_price">$${ $unitPrice }</span>
                                            <span class="old_price">$78.99</span>
                                        </div>
                                        <div class="modal_description mb-15">
                                        </div>
                                        <div class="variants_selects">
                                            <div class="modal_add_to_cart">
                                                <form action="#">
                                                    <input type="number" min="0" step="1" onclick="ChangeQuantityOfProduct(this.value, '${ $formId }')" value="${ (quantity !== null ? quantity : 1) }" />
                                                    <button type="button" onclick="ProductModalSubmit('${ $formId }')">add to cart</button>
                                                </form>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `);

    // #endregion

    $("#modal-product").modal("show");
};

ClearProductModal = () => {
    $("#modal-product").remove();
    $(".modal-backdrop").remove();
    $("body").removeClass("modal-open");
    $("body").removeProp("style");
};

ChangeQuantityOfProduct = (quantity, formId) => {
    $("#" + formId).find("[name='QuantityOfItem']").val(quantity);
};

ProductModalSubmit = (formId) => {
    let $form = $("#" + formId);

    //let quantityOfItem = $form.find("[name='QuantityOfItem']").val();

    //if (quantityOfItem !== "") {
    //    $form.prop("action", "/Cart/UpdateQuantity");
    //}

    $("#" + formId).find("[name='btn-submit']").click();
};

// #endregion

// #region Render
  
// #endregion

// #region Affect

// #endregion

// #region Action

// #endregion

// #region Validate

// #endregion

// #region Response

// #endregion