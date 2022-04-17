// script to filter inf paths
function searchTable() {

    var input, filter, table, row, tableData, index, txtVal;
    input = document.getElementById("search");
    filter = input.value.toUpperCase();
    table = document.getElementById("seatTable");
    row = table.getElementsByTagName("tr");

    for (index = 1; index < row.length; index++) {
        tableData = row[index].getElementsByTagName("td")[2];
        if (tableData) {
            txtVal = tableData.textContent || tableData.innerText;
            if (txtVal.toUpperCase().indexOf(filter) > -1) {
                row[index].style.display = "";
            }
            else {
                row[index].style.display = "none";
            }
        }
    } // end for
} //end function 