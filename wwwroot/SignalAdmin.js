const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

hubConnection.on("Send", function (data) {

    let elem = document.createElement("p");
    elem.classList.add("text");
    elem.setAttribute("onclick", 'copy("' + data + '")')
    elem.appendChild(document.createTextNode(data));
    let firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);

});

document.getElementById("sendBtn").addEventListener("click", function (e) {
    hubConnection.invoke("Send", "Mod");
});

hubConnection.start();

function copy(text) {
    var copyTextarea = document.createElement("textarea");
    copyTextarea.style.position = "fixed";
    copyTextarea.style.opacity = "0";
    copyTextarea.textContent = text;

    document.body.appendChild(copyTextarea);
    copyTextarea.select();
    document.execCommand("copy");
    document.body.removeChild(copyTextarea);
}