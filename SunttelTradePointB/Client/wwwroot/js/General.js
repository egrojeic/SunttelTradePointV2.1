
var ref = null;
let modeOnOff = false;
window.LoadReference = function (_ref) {
    ref = _ref;
};

window.OnKey = new function () {
    alert();
}
console.log("si...");

document.addEventListener("keyup", function (event) {
    if (event.key === 'Enter') {
        ref.invokeMethodAsync("SaveEditCell");
        alert('Enter is pressed!');
    }
});

document.addEventListener("keydown", function (event) {
    if (event.keyCode === 13) {
        // Aquí puedes realizar las acciones que deseas al presionar Enter
        console.log("Presionaste Enter");
        alert('Enter is pressed2!');
        // Evita el comportamiento por defecto del Enter en formularios
        event.preventDefault();
    }
});







