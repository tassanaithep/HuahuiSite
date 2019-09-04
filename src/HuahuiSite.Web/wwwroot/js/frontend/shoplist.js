// #region On Load

/**
  * @desc On Page Load
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
$(function () {
    $("#table-data").DataTable();
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
    let $productId = parseInt($form.find("[name='ProductId']").val());
    let $name = $form.find("[name='ProductName']").val();
    let $unitPrice = parseInt($form.find("[name='ProductUnitPrice']").val());
    let $pictureFileName = $form.find("[name='ProductPictureFileName']").val();
    let $isPromotion = JSON.parse($form.find("[name='IsPromotion']").val());
    let $promotionPrice = $form.find("[name='PromotionPrice']").val();

    let $quantityOfItem = $form.find("[name='QuantityOfItem']");

    // #endregion

    // #region Get Quantity of Product

    let quantity = null;

    if (_cartItemList !== null) {
        for (var i = 0; i < _cartItemList.length; i++) {
            if (_cartItemList[i].productId === $productId) {
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

    // #region Create Modal of Product

    $("body").append(`
        <div class="modal fade" id="modal-product" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
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
                                        ${
                                            (
                                                !$isPromotion ? `
                                                    <span class="new_price">${ $unitPrice } บาท</span>`
                                                :
                                                    `<span class="new_price">${ $promotionPrice } บาท</span>
                                                    <span class="old_price">${ $unitPrice } บาท</span>`
                                            )    
                                        }
                                        </div>
                                        <div class="modal_description mb-15">
                                        </div>
                                        <div class="variants_selects">
                                            <div class="modal_add_to_cart">
                                                <form action="#">
                                                    <input type="number" step="1" min="1" onkeyup="ChangeQuantityOfProduct('${ $formId }', this.value)" onclick="ChangeQuantityOfProduct('${ $formId }', this.value)" value="${ (quantity !== null ? quantity : 1) }" />
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

    // Bind Price of Product By Quantity of Product Group
    ChangeProductPriceByQuantity($formId, quantity !== null ? quantity : 1);

    $("#modal-product").modal("show");
};

/**
  * @desc Clear Modal of Product
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ClearProductModal = () => {
    $("#modal-product").remove();
    $(".modal-backdrop").remove();
    $("body").removeClass("modal-open");
    $("body").removeProp("style");
};

/**
  * @desc Change Quantity of Product
  * @param {String} formId - Id Name of Product Form
  * @param {Number} quantity - Quantity of Product
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ChangeQuantityOfProduct = (formId, quantity) => {
    $("#" + formId).find("[name='QuantityOfItem']").val(quantity);

    ChangeProductPriceByQuantity(formId, quantity);
};

/**
  * @desc Change Product Price By Quantity
  * @param {String} formId - Id Name of Product Form
  * @param {Number} quantity - Quantity of Product
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ChangeProductPriceByQuantity = (formId, quantity) => {
    let $productGroupCode = $("#" + formId).find("[name='ProductGroupCode']").val();
    let $isPromotion = JSON.parse($("#" + formId).find("[name='IsPromotion']").val());

    $.ajax({
        type: "GET",
        url: "/Home/GetProductPriceByQuantity",
        data: { productGroupCode: $productGroupCode, quantity: quantity },
        success: function (res) {
            let productGroupModel = res;

            let unitPrice = productGroupModel.unitPrice;
            let promotionPrice = productGroupModel.promotionPrice;

            $("#" + formId).find("[name='ProductUnitPrice']").val(unitPrice);
            $("#" + formId).find("[name='ProductPromotionPrice']").val(promotionPrice);

            if (!$isPromotion) {
                $("#modal-product").find(".new_price").text(unitPrice + " บาท");
            }
            else {
                $("#modal-product").find(".old_price").text(unitPrice + " บาท");
                $("#modal-product").find(".new_price").text(promotionPrice + " บาท");
            }
        },
        error: function () { }
    });
};

/**
  * @desc Submit Form Product form Modal
  * @param {String} formId - Id Name of Product Form
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ProductModalSubmit = (formId) => {
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