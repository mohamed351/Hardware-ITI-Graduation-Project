$(document).ready(function () {
    dataColumns = [{ data: "name" }, {
        data: "id", render: function (data) {
            return `<button class="btn btn-primary edit-button" product=${data}> Edit </button> |
                                <button class="btn btn-danger delete-button"  product=${data}> Delete </button>`
        }
    }];

    tbl = tablePlugin("#ProductTable", "/Admin/Products/GetProducts", dataColumns);

    $("#btn").click(function () {
        console.log('clickeeeeeed')
        $.ajax({
            url: "/admin/Products/Create",
            success: function (result) {
                $("#productcontent").html(result);
                $("#productModel").modal("show");
                var form = $("form");
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);
            }
        })
    });

    $("#productcontent").on("change", "#CategoryId", function () {
        $("#ModelId").empty();
        $("#productAttributes").empty();
        $("#ModelId").append("<option value=''>Select Model</option>");
        $.ajax({
            type: 'POST',
            url: "/Admin/Products/GetModelWithCategories",
            data: { categoryId: $("#CategoryId").val() },
            success: function (data) {
                console.log(data);
                $.each(data, function (i, item) {
                    $("#ModelId").append(`<option value="${item.id}">${item.name}</option>`);
                });
            }
        }),
            $.ajax({
                type: 'POST',
                url: "/Admin/Products/GetCategoryAttributes",
                data: { categoryId: $("#CategoryId").val() },
                success: function (data) {
                    //console.log(data);
                    $("#productAttributes").append(data);
                }
            });


    });

    $("#productcontent").on("click", "#submitform", function () {
        if ($(this).valid()) {
            $("#productModel").modal("hide");
        }
    });

    $("#ProductTable").on("click", ".delete-button", function () {
        var ID = $(this).attr("product");
        $.ajax({
            url: "/admin/Products/Delete/",
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

    $("#ProductTable").on("click", ".edit-button", function () {
        var id = $(this).attr("product");
        $.ajax({
            url: "/admin/Products/Edit/" + id,
            success(data) {
                $("#productcontent").html(data);
                $("#productModel").modal("show");
                var form = $("form");
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);
            }
        });

    });

    Complated = function (xhr) {

        if (xhr.status == 200) {
            tbl.ajax.reload();
            $("#productModel").modal("hide");
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
});

