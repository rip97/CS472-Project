// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function filterSeats() {

    
    var input, filter, table, tRow, tData;

    input = document.getElementById("filter");
    filter = input.nodeValue.toUpperCase();
    table = document.getElementById("seatTable");
    tRow = table.getElementsByTagName("tr");

    for (var i = 0; i < tRow.length; i++) {
        tData = tRow[i].getElementsByTagName("td")[2];
        if (tData) {
            txtValue = tData.textContent || tData.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tRow[i].style.display = "";
            }
            else {
                tRow[i].style.display = "none"
            }
        }

    }
}
