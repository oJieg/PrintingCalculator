const tooltiptext = document.getElementById("tooltiptext");
function copyСlipboard() {
	navigator.clipboard.writeText('Стоимость изготовления на бумаге @Model.PaperResult.NamePaper - @Model.PaperResult.Price руб.')
}

function CopyPrice(price) {
	navigator.clipboard.writeText(price)
	tooltiptext.classList.remove("tooltiptext");
	tooltiptext.classList.add("tooltiptext2");
	setTimeout(editClass, 3000);
}
function editClass() {
	tooltiptext.classList.remove("tooltiptext2");
	tooltiptext.classList.add("tooltiptext");
}
