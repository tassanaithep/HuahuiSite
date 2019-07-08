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
  * @desc opens a modal window to display a message
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
ClearForm = () => {
    $("#form-create")[0].reset();
    $(".dropify-preview, .dropify-clear").remove();
};

/**
  * @desc opens a modal window to display a message
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
HideModal = () => {
    $(".modal").modal("hide");
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
};

/**
  * @desc opens a modal window to display a message
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdateTable = () => {
    $.ajax({
        url: "/Backend/Product/UpdateTable",
        success: function (res) {
            $("#parent-table").html(res);
        },
        error: function () { }
    });
};

// #endregion

// #region Action

/**
  * @desc opens a modal window to display a message
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
                UpdateTable();
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
        $("body").removeAttr("style");
        HideModal();
        ClearForm();
        UpdateTable();
        swal("Save Success", "", "success");
        DropifyScriptRender();
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
        HideModal();
        ClearForm();
        UpdateTable();
        swal("Update Success", "", "success");
        DropifyScriptRender();
    } else {
        swal("Update Success", "", "error");
    }
};

// #endregion