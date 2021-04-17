window.onload = function () {
    var AddList = document.getElementsByClassName("AddToCart");
    var DeduceList = document.getElementsByClassName("DeduceFromCart");
    for (let i = 0; i < AddList.length; i++)
        AddList[i].addEventListener("click", onClick1);
    for (let i = 0; i < DeduceList.length; i++)
        DeduceList[i].addEventListener("click", onClick2);
}

function onClick1(event) {
    let elem = event.currentTarget;
    let productid = elem.getAttribute("productId");
    let xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE && this.status === 200) {
            let data = JSON.parse(this.responseText);
            elem.innerHTML = data.message;
            document.getElementById("shoppingCartCount").innerHTML = "<span class=\"notify-badge\">"+data.count+"</span>";
            document.getElementById(productid).innerHTML = data.productCount;
            document.getElementById("totalPrice").value = data.totalprice;
        }
    };

    xhr.open("POST", "/Home/AddToCart?id=" + productid, true);
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf8");
    xhr.send();
}

function onClick2(event) {
    let elem = event.currentTarget;
    let productid = elem.getAttribute("productId");
    let row = elem.parentNode.parentNode.rowIndex;


    let xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE && this.status === 200) {
            let data = JSON.parse(this.responseText);
            document.getElementById(productid).innerHTML = data.productCount;
            document.getElementById("totalPrice").value = data.totalprice;

            if (data.count == 0)
                document.getElementById("shoppingCartCount").innerHTML = "";
            else
                document.getElementById("shoppingCartCount").innerHTML = "<span class=\"notify-badge\">" + data.count + "</span>";           

            if (data.productCount == 0) {
                document.getElementById("table1").deleteRow(row);
            }
            if (document.getElementById("table1").rows.length == 0) {
                document.getElementById("cartlist").innerHTML = "<p>Shopping Cart is empty! Click Continue Shopping and Add Items to Cart!</p>";
                document.getElementById("price").innerHTML = "";

            }

        }
    };

    xhr.open("POST", "/Cart/Deduce?id=" + productid, true);
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf8");
    xhr.send();
}