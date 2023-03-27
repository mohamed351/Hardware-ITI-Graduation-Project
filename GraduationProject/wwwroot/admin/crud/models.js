$(document).ready(function () {

    dataColumns = [{ data: "name" }, {
        data: "id", render: function (data) {
            return `<button   class="btn btn-primary edit-button" models=${data}> Edit </button> |
                            <button class="btn btn-danger delete-button"  models=${data}> Delete </button>`
        }
    }];

    tbl = tablePlugin("#modelstbl", "/admin/Models/GetModals", dataColumns);

    $("#btn").click(function () {
        $.ajax({
            url: "/admin/Models/Create",
            success: function (data) {
                $("#modelcontent").html(data);
                $("#ModelModel").modal("show");
                var form = $("form");
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);

            }
        });
    });


    $("#modelstbl").on("click", ".edit-button", function () {
        var id = $(this).attr("models");
        $.ajax({
            url: "/admin/Models/Edit/" + id,
            success(data) {
                $("#modelcontent").html(data);
                $("#ModelModel").modal("show");
                var form = $("form");
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);
            }
        });

    });



    $("#modelstbl").on("click", ".delete-button", function () {
        var ID = $(this).attr("models");
        $.ajax({
            url: "/admin/models/Delete/",
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
            $("#ModelModel").modal("hide");
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