function clicTable(){
//alert();
}
function productionClick(idProduction){
    //alert(idProduction);
}

function closeOrder(orderId){
alert("close"+ orderId)
}
function openOrder(orderId){
    alert("open" + orderId)
}



async function loadPage() {
    let respone =await getListOrder(1,0,2);
    //console.log(respone);

    generatorTable(respone);
    await test();
 //   console.log( await test());
}

async function generatorTable2(inResponseOrder) {
    console.log(inResponseOrder);
}

async function test(){
    var settings = {
        "url": "https://localhost:7181/api/order/get-order5",
        "method": "GET",
        "timeout": 0,
        "headers": {
          "accept": "text/plain"
        },
      };
      
      $.ajax(settings).done( async function (response) {
       await generatorTable2(response);
      });
}

async function getListOrder(status, skip,take) {
    let respone1 = await fetch('https://localhost:7181/api/order/get-list-open-order?statusOrder='+status+'&skip='+skip+'&take='+ take, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify()
    });
    responseOrder = await respone1.json();
    return responseOrder.result.$values;
}

