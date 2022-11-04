const tooltiptext = document.getElementById("tooltiptext");
function copyСlipboard() {
    copy('Стоимость изготовления на бумаге @Model.PaperResult.NamePaper - @Model.PaperResult.Price руб.')
}

function CopyPrice(price) {
    copy(price + "руб.")
    //navigator.clipboard.writeText(price + "руб.")
    tooltiptext.classList.remove("tooltiptext");
    tooltiptext.classList.add("tooltiptext2");
    setTimeout(editClass, 3000);
}
function editClass() {
    tooltiptext.classList.remove("tooltiptext2");
    tooltiptext.classList.add("tooltiptext");
}

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