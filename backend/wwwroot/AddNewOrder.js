async function addNewOrderHref(){
    //let idOrder = await apiNewOrder();
   // console.log(idOrder)
    window.location.href = '/order/detal?id=' + await apiNewOrder();
}

async function apiNewOrder(){
    let respone1 = await fetch('/api/order/add-new-order', {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0) {
        alert("не получилось получить Product!" + responseOrder.status.errorMassage)
    }
    return responseOrder.result;
}