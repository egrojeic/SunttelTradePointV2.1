
window.ChatScroll = function () {
    window.scrollTo(0, 0);
  
}



window.speak = function (value) {
    var text = new SpeechSynthesisUtterance(value);
    speechSynthesis.speak(text);
}






