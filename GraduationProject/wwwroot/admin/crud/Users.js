$(document).ready(function () {
    dataColumns = [{ data: "userName" }, {
        data: "id", render: function (data) {
            return `<button class="btn btn-primary edit-button" users=${data}> Edit </button> |
                                <button class="btn btn-danger delete-button"  users=${data}> Delete </button>`
        }
    }];

    tbl = tablePlugin("#userstable", "/Admin/Users/GetUsers", dataColumns);

    $("#btn").click(function () {
        $.ajax({
            url: "/admin/Users/Create",
            success: function (data) {
                $("#userscontent").html(data);
                $("#usersModel").modal("show");
                var form = $("form");
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);

            }
        });
    });

    $("#userscontent").on("click", "#submitform", function () {
        if ($(this).valid()) {
            $("#usersModel").modal("hide");
        }
    });


    $("#userstable").on("click", ".delete-button", function () {
        var ID = $(this).attr("users");
        $.ajax({
            url: "/admin/Users/Delete/",
            data: { id: ID },
            method: "DELETE",
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


    $("#userstable").on("click", ".edit-button", function () {
        var id = $(this).attr("users");
        $.ajax({
            url: "/admin/Users/Edit/" + id,
            success(data) {
                $("#userscontent").html(data);
                $("#usersModel").modal("show");
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
            $("#usersModel").modal("hide");
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

