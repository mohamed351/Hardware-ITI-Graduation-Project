$(document).ready(function () {


    dataColumns = [{ data: "areaName" }, {
        data: "id", render: function (data) {
            return `<button   class="btn btn-primary edit-button" area=${data}> Edit </button> |
                            <button class="btn btn-danger delete-button"  area=${data}> Delete </button>`
        }
    }];
    tbl = tablePlugin("#areatbl", "/admin/Areas/GetAreas", dataColumns);

    $("#btn").click(function () {
        $.ajax({
            url: "/admin/Areas/Create",
            success: function (data) {
                $("#areacontent").html(data);
                $("#areaModel").modal("show");
                var form = $("form");
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);

            }

        });




    });
    $("#areacontent").on("submit", "form", function () {

        if ($(this).valid()) {
            $("#areaModel").modal("hide");

        }

    });

    $("#areatbl").on("click", ".edit-button", function () {
        var id = $(this).attr("area");
        $.ajax({
            url: "/admin/Areas/Edit/" + id,
            success(data) {
                $("#areacontent").html(data);
                $("#areaModel").modal("show");
                var form = $("form");
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);
            }
        });

    });


    $("#areatbl").on("click", ".delete-button", function () {
        var ID = $(this).attr("area");
        $.ajax({
            url: "/admin/Areas/Delete/",
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


    Complated = function (xhr) {

        if (xhr.status == 200) {
            tbl.ajax.reload();
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