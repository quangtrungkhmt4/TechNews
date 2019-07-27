var categoryController = {
    currentCategory: 0,
    showFormAddNew: function () {
        $.ajax({
            method: "GET",
            url: "/admin/Categories/createoredit",
            async: false,
            data: {
                id: categoryController.currentCategory
            }
        }).done(function (response) {
            $('.modal').remove();
            $("body").append(response);
            $("button[name=add-new]").click();
        });
    },
    deleteCategory: function () {
        $.ajax({
            method: "POST",
            url: "/admin/category/delete",
            data: {
                id: categoryController.currentCategory
            },
            async: false
        }).done(function (response) {
            window.location.reload();
        });
    },
    saveData: function () {
        var model = {
            Id: $("#Id").val(),
            Name: $("#Name").val(),
            Description: $("#Description").val(),
            ParentId: $("#Parent").val(),
            IsShowHomePage: $("input[name=Active]:checked").val() === "true" ? true : false,
            OrderDisplay: $("#OrderDisplay").val(),
            Status: $("#Status").val()
        };
        var vld = window.baseJs.validation;
        var $form = $("#createoredit");
        var jsonData = JSON.stringify(model);
        $.ajax({
            url: '/admin/Categories/createoredit',
            data: jsonData,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            cache: false,
            success: function (response) {
                if (response.result === true) {
                    $('#con-close-modal').modal('toggle');
                    window.location.reload();
                } else if (response.Errors) {
                    vld.clear($form);
                    vld.all(response.Errors, $form);
                } else {
                    $('#con-close-modal').modal('toggle');
                }
            }
        });
    },
};