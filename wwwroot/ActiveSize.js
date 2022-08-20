let Height = 297;
let Width = 420;

const ClassFastHover = "FastHover";
const ClassDefaultHover = "FastSize";

function NewSize(size, side) {
    if (side == "Height") {
        Height = size;
    }

    if (side == "Width") {
        Width = size;
    }
    activeButtonSize()
}

function editSize(HeightPaper, WidthPaper) {
    document.getElementById("Height").value = HeightPaper;
    document.getElementById("Width").value = WidthPaper;
    Height = HeightPaper;
    Width = WidthPaper;
    activeButtonSize()
}

function activeButtonSize() {
    let elementFastSize = document.querySelectorAll("div.FastSizeBlock > input");

    let idSize;
    if (Height > Width) {
        idSize = `${Width}${Height}`;
    }
    else {
        idSize = `${Height}${Width}`;
    }

    for (let item of elementFastSize) {
        if (item.id == idSize) {
            item.className = ClassFastHover;
        }
        else {
            if (item.className != ClassDefaultHover) {
                item.className = ClassDefaultHover;
            }
        }
    }
}