
var ref = null;
window.LoadReferenceGeneral = function (_ref) {
    ref = _ref;
};




document.addEventListener("keyup", function (event) {
    if (event.key === 'Enter') {
        ref.invokeMethodAsync("SetEventEnter");
       //alert('Enter is pressed!');
    }
});









