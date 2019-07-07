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

ChangeImageFile = (element, formId) => {
    let $inputPictureFile = $("#" + formId).find(".title-picture-file-update");

    let file = element.files[0];
    let reader = new FileReader();

    reader.onload = (function (theFile) {
        return function (e) {
            let binaryData = e.target.result;

            let base64String = "data:image/jpeg;base64," + window.btoa(binaryData);

            $inputPictureFile.val(base64String);
        };
    })(file);

    reader.readAsBinaryString(file);
};


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