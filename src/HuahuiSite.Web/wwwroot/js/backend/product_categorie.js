// #region On Load

/**
  * @desc On Page Load
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
$(function () {
    //RenderImage();
    $("#table-data").DataTable();
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

// #endregion

// #region Affects

/**
  * @desc Update Page
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
UpdatePage = () => {
    HideModal();
    ClearForm();
    UpdateTable();
    //DropifyScriptRender();
    //RenderImage();
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
        url: "/Backend/ProductCategorie/UpdateTable",
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
  * @desc Delete Data
  * @param {Object} e - Element of Delete Button
  * @author Mod Nattasit mod.nattasit@gmail.com
*/
Delete = (e) => {

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
            let $form = $(e).closest("tr").find(".form-row-table");

            let $id = $form.find("[name='Id']").val();
            //let $titlePictureFileName = $form.find("[name='TitlePictureFileName']").val();

            let jsonObject = {};
            jsonObject.Id = $id;
            //jsonObject.TitlePictureFileName = $titlePictureFileName;

            $.ajax({
                type: "POST",
                url: "/Backend/ProductCategorie/Delete",
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