const tooltiptext = document.getElementById("tooltiptext");
const tooltiptext3 = document.getElementById("tooltiptext3");
function copyСlipboard(paperName, price) {
    copy("Стоимость изготовления на бумаге " + paperName + " - " + price + " руб.")
}

function CopyPrice(price) {
    copy(price + " руб.")
    document.getElementById("tooltiptext").classList.remove("tooltiptext");
    document.getElementById("tooltiptext").classList.add("tooltiptext2");
    setTimeout(editClass, 3000);
}
function editClass() {
    document.getElementById("tooltiptext").classList.remove("tooltiptext2");
    document.getElementById("tooltiptext").classList.add("tooltiptext");
}
function editClass2() {
    tooltiptext3.classList.remove("tooltiptext2");
    tooltiptext3.classList.add("tooltiptext");
}

function copy(text) {
    let copyTextarea = document.createElement("textarea");
    copyTextarea.style.position = "fixed";
    copyTextarea.style.opacity = "0";
    copyTextarea.textContent = text;

    document.body.appendChild(copyTextarea);
    copyTextarea.select();
    document.execCommand("copy");
    document.body.removeChild(copyTextarea);
}

async function putComment(id) {
    let comment = document.getElementById("comment");

    let respone1 = await fetch('/api/add-comment', {
        method: "put",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({ Id: parseInt(id), comment: comment.value })
    });
    if (!respone1.json()) {
        alert("Критическая ошибка, обратитесь к Олегу!")
    }
    else {
        tooltiptext3.classList.remove("tooltiptext");
        tooltiptext3.classList.add("tooltiptext2");
        setTimeout(editClass2, 3000);
    }
    ;
}