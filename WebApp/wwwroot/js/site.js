// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".carousel").carousel();

    $("#uploadBtn").on("click", function () {
        let formData = new FormData();
        let fileList = $("#file")[0].files;
        for (let key in fileList) {
            formData.append("file", fileList[key]);
        }
        $.ajax({
            url: "/api/Asset",
            type: "post",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            error: function (data) {
                console.log(data);
            },
            success: function (responce) {//в responce массив с id картинок
                if (responce != null && responce.length > 0) {
                    $(".carousel-inner").html("");
                    for (let i in responce) {
                        $(".my_images").append("<input name='Product.ImageIds[" + i + "]' value='" + responce[i] + "' />");

                        $(".carousel-inner").append("<div class='carousel-item'>\
                                <img class='d-block w-100'\
                                src = '/api/Asset/"+ responce[i] + "' alt='slide'/></div>");
                    }
                    $(".carousel-inner .carousel-item").first().addClass("active");
                }
            }
        })
    })
});