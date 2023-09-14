
let maxCountClu = 5;
$('#Name').blur(function () {
    if ($(this).val() == '') {
        $('#clue').fadeOut();
    }
});
$('#Phone').blur(function () {
    if ($(this).val() == '') {
        $('#clue').fadeOut();
    }
});
$('#Email').blur(function () {
    if ($(this).val() == '') {
        $('#clue').fadeOut();
    }
});


$('#Email').on('input', async function () {
    $('#clue').fadeIn();

    if ($(this).val() != '') {
        renderClue(await findContactForEmail($(this).val()));
    }
});
$('#Phone').on('input', async function () {
    $('#clue').fadeIn();

    if ($(this).val() != '') {
       // console.log(await findContactForPhone($(this).val()));
        renderClue(await findContactForPhone($(this).val()));
    }
});
$('#Name').on('input', async function () {
    $('#clue').fadeIn();
    
    if ($(this).val() != '') {
       // console.log(await findContactForName($(this).val()));
        renderClue(await findContactForName($(this).val()));
    }
});

function renderClue(contacts){
    $('#clue').empty();

    if(contacts != null && contacts.$values.length>0){
        for(let i = 0; i<contacts.$values.length && i<maxCountClu ; i++){
            renderContactClue(contacts.$values[i]);
        }
    }

    $('#clue').append('<div class="clueContact" id="newContact">Добавить как новый контакт</div>');
}

function renderContactClue(contact){
    let phine = "";
    let mail = "";
    if (contact.phoneNmbers != null && contact.phoneNmbers.$values.length>0) {
        phine = contact.phoneNmbers.$values[0].number;
    }
    if (contact.mails != null && contact.mails.$values.length>0 ) {
        mail = contact.mails.$values[0].email;
    }

    $('#clue').append( '<div class="clueContact" onclick="getContact('+contact.id+'">'+
    contact.name + '|'+ phine +'|'+ mail + '</div>');
}

async function findContactForPhone(phone) {
    let respone1 = await fetch('https://localhost:7181/api/contact/searth-contact-by-phone?phone=' + phone, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0 && responseOrder.status != 4) {
        alert("не получилось получить Product!" + responseOrder.status.errorMassage)
    }
    return responseOrder.result;
}

async function findContactForEmail(Email) {
    let respone1 = await fetch('https://localhost:7181/api/contact/searth-contact-by-email?email=' + Email, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0 && responseOrder.status != 4) {
        alert("не получилось получить Product!" + responseOrder.status.errorMassage)
    }
    return responseOrder.result;
}

async function findContactForName(name) {
    let respone1 = await fetch('https://localhost:7181/api/contact/searth-contact-by-name?name=' + name, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0 && responseOrder.status != 4) {
        alert("не получилось получить Product!" + responseOrder.status.errorMassage)
    }
    return responseOrder.result;
}
