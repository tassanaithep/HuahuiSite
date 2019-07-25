// #region On Load

/**
  * @desc On Page Load
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
$(function () {
    RenderImage();
    BindData();
});

// #endregion

// #region Render

/**
  * @desc Render Image of Datatable
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
RenderImage = () => {
    $(".form-row-table").each(function (index, element) {
        let $pictureFileName = $(element).find("[name='PictureFileName']").val();

        if ($pictureFileName !== "") {
            $(element).find(".dropify-wrapper").addClass("has-preview");
            $(element).find(".dropify-preview").css("display", "block");
            $(element).find(".dropify-render").append("<img src='/images/upload/" + $pictureFileName + "' />");
        }
    });
};

/**
  * @desc Bind Data
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
BindData = () => {
    $(".form-row-table").each(function (index, element) {
        // #region Get Element

        let $unitIdSelect = $(element).find("[name='UnitId']");
        let $productCategorieCodeSelect = $(element).find("[name='ProductCategorieCode']");
        let $productGroupCodeSelect = $(element).find("[name='ProductGroupCode']");

        // #endregion

        // #region Get Value

        let $unitId = $(element).find("[name='hid-unitId']").val();
        let $productCategorieCode = $(element).find("[name='hid-productCategorieCode']").val();
        let $productGroupCode = $(element).find("[name='hid-productGroupCode']").val();

        // #endregion

        // #region Binding Value

        $unitIdSelect.val($unitId);
        $productCategorieCodeSelect.val($productCategorieCode);
        $productGroupCodeSelect.val($productGroupCode);

        // #endregion
    });
};

// #endregion

// #region Functions

// #region On Click

/**
  * @desc On Checkbox Click
  * @param {Object} e - Element of Checkbox
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
SwitchCheck = (e) => {
    let $elementName = $(e).prop("id").substr(0, $(e).prop("id").indexOf('-'));

    let $elementOfValue = $(e).closest(".form-row-table").find("[name='" + $elementName + "']");

    if ($elementOfValue.val() === "true") {
        $elementOfValue.val("false");
    }
    else if ($elementOfValue.val() === "false") {
        $elementOfValue.val("true");
    }
};

// #endregion

// #endregion

// #region Affect

/**
  * @desc Update Page
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdatePage = () => {
    HideModal();
    ClearForm();
    UpdateTable();
    DropifyScriptRender();
    RenderImage();
    BindData();

    $("body").removeAttr("style");
};

/**
  * @desc Clear Create Form
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ClearForm = () => {
    $("#form-create")[0].reset();
    $("#form-create").find(".dropify-wrapper").remove();
    $("#parent-form-group-picture").append("<input class='dropify' type='file' id='PictureImageFile' name='PictureImageFile'>");
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
        url: "/Backend/Product/UpdateTable",
        async: false,
        success: function (res) {
            $("#parent-table").html(res);
        },
        error: function () { }
    });
};

// #endregion

// #region Action

/**
  * @desc Delete Data
  * @param {Object} e - Element of Delete Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
Delete = (e) => {
    let $form = $(e).closest("tr").find(".form-row-table");

    let $id = $form.find("[name='Id']").val();
    let $pictureFileName = $form.find("[name='PictureFileName']").val();

    let jsonObject = {};
    jsonObject.Id = $id;
    jsonObject.PictureFileName = $pictureFileName;

    $.ajax({
        type: "POST",
        url: "/Backend/Product/Delete",
        data: jsonObject,
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
};

// #endregion

// #region Validate

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

// #region Response

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