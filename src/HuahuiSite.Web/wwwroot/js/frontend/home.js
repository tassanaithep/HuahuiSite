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
  * @desc Open Picture Modal of Product
  * @param {Object} e - Element of Open Modal Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
OpenProductPictureModal = (e) => {
    // #region Get Value form Form

    let $form = $(e).closest(".form-product-item");

    let $productName = $form.find("[name='ProductName']").val();
    let $pictureFileName = $form.find("[name='ProductPictureFileName']").val();

    // #endregion

    // #region Create Product Picture Modal

    $("body").append(`
        <div class="modal fade" id="modal-product-picture" tabindex="-1" role="dialog" style="display: none;" aria-hidden="true">
	        <div class="modal-dialog modal-lg" role="document">
		        <div class="modal-content text-center">
			        <div class="modal-header">
				        <h4 class="modal-title" id="myModalLabel">${ $productName }</h4>
				        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="ClearProductPictureModal()">×</button>
			        </div>
			        <div class="modal-body">
				        <img src="/images/upload/${ $pictureFileName }" alt="" width="600">
			        </div>
		        </div>
	        </div>
        </div>
    `);

    // #endregion

    // Show Product Picture Modal
    $("#modal-product-picture").modal("show");
};

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
    let $minQuantity = $form.find("[name='MinQuantity']").val();
    let $maxQuantity = $form.find("[name='MaxQuantity']").val();

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
                                                    <input type="number" min="${ $minQuantity}" max="${ $maxQuantity }" step="1" onkeyup="ChangeQuantityOfProduct(this, '${ $formId }')" onclick="ChangeQuantityOfProduct(this, '${ $formId }')" value="${ (quantity !== null ? quantity : 1) }" />
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

    // Show Product Picture Modal
    $("#modal-product").modal("show");
};

/**
  * @desc Change Quantity of Product
  * @param {Object} e - Element of Input Quantity
  * @param {String} formId - Id Name of Product Form
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ChangeQuantityOfProduct = (e, formId) => {
    let inputQuantity = parseInt($(e).val());
    let maxQuantityOfProduct = parseInt($(e).prop("max"));

    // #region Check Input Quantity more than Max Quantity

    if (inputQuantity <= maxQuantityOfProduct) {
        $("#" + formId).find("[name='QuantityOfItem']").val(quantity);
    } else {
        // If Input Quantity more than Max Quantity to Remove last Character
        $(e).val(inputQuantity.toString().substr(0, inputQuantity.toString().length - 1));
    }
};

/**
  * @desc Clear Modal of Product
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ClearProductPictureModal = () => {
    $("#modal-product-picture").remove();
    $(".modal-backdrop").remove();
    $("body").removeClass("modal-open");
    $("body").removeProp("style");
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