
var ref = null;
let modeOnOff = false;
window.LoadReference = function (_ref) {
    ref = _ref;
};


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
    synthesisUtterance.rate = 0.9;
    synthesisUtterance.pitch = 1;
    synthesisUtterance.volume = 8;
    synthesisUtterance.lang = "en-US";
    //synthesisUtterance.lang = "en-AU";  
    //synthesisUtterance.lang = "en-CA";      
    speechSynthesis.speak(synthesisUtterance);
}

let speechRecognition;
window.Recognition = function (mode) {
    modeOnOff = mode;    
    speechRecognition.start();
}

let LoadRecognition = function () {
    let value = "";
    if (!("webkitSpeechRecognition" in window)) {
        console.log('No soportado.');
    } else {
        speechRecognition = new (window.SpeechRecognition || window.webkitSpeechRecognition || window.mozSpeechRecognition || window.msSpeechRecognition)();
        speechRecognition.lang = "es-AR";
        speechRecognition.interiResults = true;
        speechRecognition.continuous = true;
        speechRecognition.interim = true;
        speechRecognition.maxAlternatives = 1;
        //speechRecognition.lang = "en-US";

        speechRecognition.onstart = function () {
            ref.invokeMethodAsync("OnstartRecognition");
        }

        speechRecognition.onresult = function (event) {
            value = "";
            for (let i = event.resultIndex; i < event.results.length; i++) {
                value += event.results[i][0].transcript;
                console.log(value);
            }
            if (value.toLowerCase().includes("finalizar")) {
                value = "";
                console.log("Apagado...finalizado")
                modeOnOff = false;
                speechRecognition.stop();
            }
            ref.invokeMethodAsync("SetTextRecognition", value);
            if (modeOnOff == false) {
                console.log("Apagado...")
                speechRecognition.stop();
            }
        }

        speechRecognition.onend = function () {
            console.log('El reconocimiento de voz ha finalizado.');
            if (modeOnOff == true) {
                Recognition(true);
            } else {
                ref.invokeMethodAsync("OnEndRecognition");
            }

        }

        speechRecognition.onerror = function (event) {
            console.error('Error de reconocimiento de voz:', event.error);
            ref.invokeMethodAsync("OnEndRecognition");
        }

    }

}

LoadRecognition();





