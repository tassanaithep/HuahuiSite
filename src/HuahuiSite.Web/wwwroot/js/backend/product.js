// #region On Load

/**
  * @desc On Page Load
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
$(function () {
    RenderImage();
});

// #endregion

// #region Render

/**
  * @desc Render Image of Datatable
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
RenderImage = () => {
    $(".form-row-table").each(function (index, element) {
        let $titlePictureFileName = $(element).find("[name='TitlePictureFileName']").val();

        if ($titlePictureFileName !== "") {
            $(element).find(".dropify-preview").css("display", "block");
            $(element).find(".dropify-clear").css("display", "block");
            $(element).find(".dropify-render").append("<img src='/images/upload/" + $titlePictureFileName + "' />");
        }
    });
};

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

    $("body").removeAttr("style");
};

/**
  * @desc Clear Create Form
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ClearForm = () => {
    $("#form-create")[0].reset();
    $("#form-create").find(".dropify-wrapper").remove();
    $("#parent-form-group-title-picture").append("<input class='dropify' type='file' id='TitlePictureImageFile' name='TitlePictureImageFile'>");
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
    let $titlePictureFileName = $form.find("[name='TitlePictureFileName']").val();

    let jsonObject = {};
    jsonObject.Id = $id;
    jsonObject.TitlePictureFileName = $titlePictureFileName;

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