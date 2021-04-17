window.onload = function () {
    var AddList = document.getElementsByClassName("AddToCart");
    for (let i = 0; i < AddList.length; i++)
        AddList[i].addEventListener("click", onClick1);
}

function onClick1(event) {
    let elem = event.currentTarget;
    let productid = elem.getAttribute("productId");
    let xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE && this.status === 200) {
            if (this.responseText.length > 200) {
                window.location = "/Identity/Account/Login";
            }
            else {
                let data = JSON.parse(this.responseText);

                let notification = document.createElement("div");
                notification.setAttribute("style", "position:fixed;top:70px;right:70px;");
                notification.className = "alert alert-success btn-sm"
                notification.innerHTML = "Successfully added to Cart";
                setTimeout(function () {
                    notification.parentNode.removeChild(notification);
                }, 2000);
                document.body.appendChild(notification);
                document.getElementById("shoppingCartCount").innerHTML = "<span class=\"notify-badge\">" + data.count + "</span>";
            }
        }
    };

    xhr.open("POST", "/Home/AddToCart?id=" + productid, true);
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf8");
    xhr.send();
}