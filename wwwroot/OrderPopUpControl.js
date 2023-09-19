let clicMiss = true;


function clicTable(orderId) {
    if (clicMiss) {
        window.location.href = '/order/detal?id=' + orderId;

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

function statusOrder(orderId, newStatus) {
    clicMiss = false;


    editStatus(orderId, newStatus);
    //$(".mainTableRow").empty();
    newLoadPage();

    // alert("close" + orderId)
}



function closeVenetka() {
    $('#vignette').fadeOut();
    $("#detalProduction").fadeOut();
    $("#detalContact").fadeOut();
    $('#detalHistory').fadeOut();
}



function searchId() {
    $("#searchId").fadeIn();
}
function searchDate() {
    $("#searchDate").fadeIn();
}

$("#searchId").change(async function (id) {
    $(".mainTableRow").empty();
    await generatorTable(await getOrderForId($(this).val()), 'table');
    $("#searchId").fadeOut();
});

$("#searchDate").change(async function (id) {
    $(".mainTableRow").empty();
    await generatorTable(await getOrderForData($(this).val()),'table');
    $("#searchDate").fadeOut();
});

