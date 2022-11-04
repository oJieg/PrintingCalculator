const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

const form = document.getElementById("inputForm");
const foto = document.getElementById("fotoMod");

hubConnection.on("Send", function (data) {
    if (data == "Mod") {
        activFormMod();
    }
});

document.getElementById("sendBtn").addEventListener("click", function (e) {
    let message = document.getElementById("message");


    hubConnection.invoke("Send", message.value);
    message.value = "";
    activFotoMod();
});
hubConnection.start();



function activFormMod() {
    foto.classList.add("noneStatus");
    form.classList.remove("noneStatus");
};

function activFotoMod() {
    form.classList.add("noneStatus");
    foto.classList.remove("noneStatus");
};

let divFoto = document.getElementById("fotoMod");
let activFoto = 0;

function nextFoto() {
    let total = divFoto.children[activFoto];
    total.classList.remove("foto");
    total.classList.add("noneStatus");
    if (activFoto >= divFoto.children.length - 1) {
        activFoto = 0;
    }
    else {
        activFoto++;
    }

    let newActiv = divFoto.children[activFoto];
    newActiv.classList.remove("noneStatus");
    newActiv.classList.add("foto");
};