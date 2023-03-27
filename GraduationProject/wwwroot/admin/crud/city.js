$(document).ready(function () {
    dataColumns = [{ data: "cityName" }, {
        data: "id", render: function (data) {
            return `<button   class="btn btn-primary edit-button" city=${data}> Edit </button> |
                            <button class="btn btn-danger delete-button"  city=${data}> Delete </button>`
        }
    }];

    tbl = tablePlugin("#citytbl", "/admin/Cities/GetCities", dataColumns);

    $("#btn").click(function () {
        $.ajax({
            url: "/admin/Cities/Create",
            success: function (data) {
                $("#citycontent").html(data);
                $("#cityModel").modal("show");
                var form = $("form");
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);

            }
        });
    });

    $("#citycontent").on("submit", "form", function () {

        if ($(this).valid()) {
            $("#cityModel").modal("hide");

        }

    });

    $("#citytbl").on("click", ".edit-button", function () {
        var id = $(this).attr("city");
        $.ajax({
            url: "/admin/Cities/Edit/" + id,
            success(data) {
                $("#citycontent").html(data);
                $("#cityModel").modal("show");
                var form = $("form");
                form.removeData('validator');
                form.removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse(form);
            }
        });

    });

    $("#citytbl").on("click", ".delete-button", function () {
        var ID = $(this).attr("city");
        $.ajax({
            url: "/admin/Cities/Delete/",
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