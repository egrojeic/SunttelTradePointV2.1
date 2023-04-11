
window.ChatScroll = function () {
    window.scrollTo(0, 0);
  
}




var voiceConfig = {
    "name": "MiVozPersonalizada",
    "lang": "es-ES",
    "localService": true,
    "voiceURI": "MiVozPersonalizada",
    "gender": "female",
    "localService": true,
    "default": false,
    "extensions": {
        "custom-voice": {
            "wav": ["ruta/al/archivo/de/audio1.wav", "ruta/al/archivo/de/audio2.wav"],
            "metadata": {
                "voice": "MiVozPersonalizada",
                "description": "Mi descripción personalizada"
            }
        }
    }
};


window.speak = function (value) {
    var synthesisUtterance = new SpeechSynthesisUtterance(value);
    synthesisUtterance.text = value;
    synthesisUtterance.rate =1;
    synthesisUtterance.pitch =1;
    synthesisUtterance.volume = 8;
    synthesisUtterance.lang = "en-US";  
    
    speechSynthesis.speak(synthesisUtterance);
}






