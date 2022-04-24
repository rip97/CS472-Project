


$(window).ready(function () { cart_initCart() });
var cartObj = null;
var cartCI = -1;

function getLocalData(name) {
    return localStorage.getItem(name)
}

function setLocalData(name, value) {

    if (value != null)
        localStorage.setItem(name, value);
    else
        localStorage.removeItem(name);
}
function cart_initCart() {
    cartObj = new Array();
    cartCI = 0;

    if (getLocalData("NETSELLS_USERCART") != null) {
        cartCI = getLocalData("NETSELLS_USERCART_CI");
        cartObj = JSON.parse(getLocalData("NETSELLS_USERCART"));
        console.log(cartObj)
    }
    else {
        console.log("cart is empty")
    }
}