﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowProducts() {
    var productCount = $("#ProductsCount").val();
    var html = "";
    var i;
    for (i = 0; i < productCount; i++) {
        html +=
            '<div>' +
            '<label>Product  </label>' +
            '<input name="Name" type="text" required/>' +
            '<label>Quantity  </label>' +
            '<input name="Quantity" type="number" required/>' +
            '</div>';
    }
    console.log(html);
    document.getElementById("buttonForProduct").innerHTML = html;
    return false;
}