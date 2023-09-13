let clicMiss = true;

function clicTable(orderId) {
    if (clicMiss) {
        alert();

    }
    clicMiss = true;

}
function detal() {
    clicMiss = false;
}
function productionClick(idProduction) {

    clicMiss = false;
    $("#vignette").fadeIn();
    $("#detalProduction").fadeIn();
    detalProduction(idProduction);

    //alert(idProduction);
}

function contactClick(contactId) {
    clicMiss = false;
    $("#vignette").fadeIn();
    $("#detalContact").fadeIn();
    detalContact(contactId);
}

function closeOrder(orderId) {
    clicMiss = false;


    editStatus(orderId, 0);
    //$(".mainTableRow").empty();
    newLoadPage();

    // alert("close" + orderId)
}

function openOrder(orderId) {
    clicMiss = false;

    editStatus(orderId, 1);
   // $(".mainTableRow").empty();
    newLoadPage();
    // $("#vignette").fadeIn();
    // alert("open" + orderId)
}

function closeVenetka() {
    $('#vignette').fadeOut();
    $("#detalProduction").fadeOut();
    $("#detalContact").fadeOut();

}



function searchId() {
    $("#searchId").fadeIn();
}
function searchDate() {
    $("#searchDate").fadeIn();
}

$("#searchId").change(async function (id) {
    $(".mainTableRow").empty();
    await generatorTable(await getOrderForId($(this).val()));
    $("#searchId").fadeOut();
});

$("#searchDate").change(async function (id) {
    $(".mainTableRow").empty();
    await generatorTable(await getOrderForData($(this).val()));
    $("#searchDate").fadeOut();
});

