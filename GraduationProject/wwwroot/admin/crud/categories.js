$(document).ready(function () {
    dataColumns = [{ data: "name" }, {
        data: "id", render: function (data) {
            return `<button   class="btn btn-primary edit-button" category=${data}> Edit </button> |
                                <button class="btn btn-danger delete-button"  category=${data}> Delete </button>`
        }
    }];

    tbl = tablePlugin("#categoryTable", "/admin/Categories/GetCategories", dataColumns);

    $("#btn").click(function () {

        $.ajax({
            url: "/admin/Categories/Create",
            success: function (result) {
                $("#categorycontent").html(result);
                $("#categoryModel").modal("show");


            }
        })

    });
    $("#categorycontent").on("click", ".addbtn", function () {
        var proccessRow = $(this).parents().find(".attribute-row").clone();

        $(proccessRow.find(".addbtn")).remove();
        $(proccessRow.find(".removerbtn")).remove();
        proccessRow.removeClass("attribute-row");
        $("#container-attribute").append(proccessRow);
    });
    $("#categorycontent").on("click", ".removerbtn", function () {
        if ($("#container-attribute .row").length > 1) {
            $($("#container-attribute .row").last()).remove();
        }

    });
    $("#categorycontent").on("click", "#submitform", function () {

        var obj = { CategoryName: $("#CategoryName").val(), AttributesID: [] };
        // validation on Object
        $("#container-attribute .attribute").each(function () {

            obj.AttributesID.push($(this).val());
        });

        $.ajax({
            url: "/admin/Categories/Create",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(obj),
            success: function (result) {

            },
            error: function (jqXHR) {
                console.log(jqXHR);
            },
            complete: Complated

        });

    });
    $("#categoryTable").on("click", ".delete-button", function () {
        var ID = $(this).attr("category");
        $.ajax({
            url: "/admin/Categories/Delete/",
            data: { id: ID },
            method: "POST",
            success(data) {
                Swal.fire({
                    icon: 'success',
                    title: data,
                    showConfirmButton: false,
                    timer: 1500
                });
            },
            complete: Complated
        });

    });
    $("#categoryTable").on("click", ".edit-button", function () {
        var id = $(this).attr("category");
        $.ajax({
            url: "/admin/Categories/Edit/" + id,
            success(data) {
                $("#categorycontent").html(data);
                $("#categoryModel").modal("show");



            }
        });

    });
    Complated = function (xhr) {

        if (xhr.status == 200) {
            tbl.ajax.reload();
            $("#categoryModel").modal("hide");
            Swal.fire({
                icon: 'success',
                title: xhr.responseText,
                showConfirmButton: false,
                timer: 1500
            });

        }
        else if (xhr.status == 400) {
            Swal.fire({
                icon: 'error',
                title: xhr.responseText,
                showConfirmButton: false,
                timer: 1500
            });

        }
    }

    $("#categorycontent").on("click", "#btnEditPlus", function () {

        var element = $(".attribute-row").last().clone();
        $("#container-attribute").append(element);
    });

    $("#categorycontent").on("click", "#btnEditRemove", function () {

        if ($(".attribute-row").length > 1) {
            $($(".attribute-row").last()).remove();
        }

    });
    $("#categorycontent").on("click", "#submitformEdit", function () {
        var obj = { ID: $("#CategoryID").val(), CategoryName: $("#CategoryName").val(), AttributesID: [] };
        // validation on Object
        $("#container-attribute .attribute").each(function () {

            obj.AttributesID.push($(this).val());
        });

        $.ajax({
            url: "/admin/Categories/Edit",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(obj),
            success: function (result) {

            },
            error: function (jqXHR) {
                console.log(jqXHR);
            },
            complete: Complated

        });


    });

});