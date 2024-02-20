async function loadPage() {
    fillTableContact(await apiGetListContact());
}

function contactClick(contactId) {
    //clicMiss = false;
    $("#vignette").fadeIn();
    $("#detalContact").fadeIn();
    detalContact(contactId);
}
function closeVenetka() {
    $('#vignette').fadeOut();
    $("#detalProduction").fadeOut();
    $("#detalContact").fadeOut();
    $('#detalHistory').fadeOut();
}

function fillTableContact(contacts) {
    $(".mainTableRow").empty();
    console.log(contacts);
    if (contacts != null) {
        for (let i = 0; i < contacts.$values.length; i++) {
            $('#table tr:last').after(getRow(contacts.$values[i]));
        }
    }
}

async function apiGetListContact() {
    let respone = await fetch('/api/contact/get-first-list-contact', {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();
    if (awner.status != 0) {
        alert("не получилось получить product!" + awner.status.errorMassage)
    }

    return awner.result;
}

async function newContact() {
    let newContact = await addNewContact($('#Name').val(), $('#Phone').val(), $('#Email').val());
    //await addContactInOrder(getIdOrder(), newContact.id)
    //$('#contactSpace').append(addContactInDetalOrder(newContact));
    console.log(newContact);
    contactClick(newContact.id)
}

function getRow(contact) {

    let allPhone = "";
    if (contact.phoneNmbers != null) {

        for (let i = 0; i < contact.phoneNmbers.$values.length; i++) {
            allPhone += '<p class="contactText">' + contact.phoneNmbers.$values[i].number + '</p>';
        }
    }

    $('#contactPhone').html(allPhone);

    let allMail = "";
    if (contact.mails != null) {

        for (let i = 0; i < contact.mails.$values.length; i++) {
            allMail += '<p class="contactText">' + contact.mails.$values[i].email + '</p>';
        }
    }

    return '<tr class="mainTableRow" onclick="contactClick(' + contact.id + ')">' +
        '<td>' +
        '<div class="textTableHistory">' + contact.id + '</div>' +
        '</td>' +
        '<td>' +
        '<div class="textTableHistory">' + contact.name + '</div>' +
        '</td>' +
        '<td>' +
        '<div class="textTableHistory">' + contact.description + '</div>' +
        '</td>' +
        '<td>' +
        '<div class="textTableHistory">' + allMail + '</div>' +
        '</td>' +
        '<td>' +
        '<div class="textTableHistory">' + allPhone + '</div>' +
        '</td>' +
        '</tr>';
}

async function addNewContact(name, phone, mail) {
    if (name != "") {
        name = 'name=' + name + '&';
    }
    if (phone != "") {
        phone = 'phone=' + phone;
    }
    if (mail != "") {
        mail = 'eMail=' + mail + '&';
    }

    let respone = await fetch('/api/contact/add-new-contact?' + name + mail + phone, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    //console.log(awner);

    if (awner.status != 0) {
        alert("не получилось изменить статус!" + awner.status.errorMassage)
    }

    return awner.result;
}