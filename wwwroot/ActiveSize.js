let height = 297;
let width = 420;

const classFastHover = "FastHover";
const classDefaultHover = "FastSize";

function newSize(size, side) {
    if (side == "Height") {
        height = size;
    }

    if (side == "Width") {
        width = size;
    }
    activeButtonSize()
}

function editSize(HeightPaper, WidthPaper) {
    document.getElementById("Height").value = HeightPaper;
    document.getElementById("Width").value = WidthPaper;
    height = HeightPaper;
    width = WidthPaper;
    activeButtonSize()
}

function activeButtonSize() {
    let elementFastSize = document.querySelectorAll("div.FastSizeBlock > input");

    let idSize;
    if (height > width) {
        idSize = `${width}${height}`;
    }
    else {
        idSize = `${height}${width}`;
    }

    for (let item of elementFastSize) {
        if (item.id == idSize) {
            item.className = classFastHover;
        }
        else {
            if (item.className != classDefaultHover) {
                item.className = classDefaultHover;
            }
        }
    }
}