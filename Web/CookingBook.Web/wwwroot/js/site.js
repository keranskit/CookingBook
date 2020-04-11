// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowProducts() {
        var productCount = $("#ProductsCount").val();
        var html = "";
        var i;
        for (i = 0; i < productCount; i++) {
            html +=
                '<div>' +
                '<label>Product</label>' +
                '<input name="Name" class="form-control type="text"/>' +
                '<label>Quantity</label>' +
                '<input name="Quantity" class="form-control" type="number"/>' +
                '</div>';

        }
        console.log(html);
        document.getElementById("buttonForProduct").innerHTML = html;
        return false;
        }
