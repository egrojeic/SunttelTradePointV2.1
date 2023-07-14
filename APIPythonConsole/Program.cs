using System.Diagnostics;

// Ubicación del directorio del proyecto FastAPI
string rutaProyectoFastAPI = @"C:\Proyectos\SunttelTradePointV2.1\PDFAnalyse";

// Comando para activar el entorno virtual
string comandoActivarEntornoVirtual = @"mi_entorno\Scripts\activate";

// Comando para correr el API en el entorno
string comandoCorrerAPI = @"C:\Proyectos\SunttelTradePointV2.1\PDFAnalyse\mi_entorno\Scripts\python.exe -m uvicorn app:app --reload";

// Ejecutar los comandos en una terminal de comandos (CMD)
EjecutarComandoEnCMD(rutaProyectoFastAPI, comandoActivarEntornoVirtual);
EjecutarComandoEnCMD(rutaProyectoFastAPI, comandoCorrerAPI);

static void EjecutarComandoEnCMD(string rutaDirectorio, string comando)
{
    ProcessStartInfo startInfo = new ProcessStartInfo
    {
        FileName = "cmd.exe",
        Arguments = "/C " + comando,
        UseShellExecute = false,
        WorkingDirectory = rutaDirectorio
    };

    Process process = new Process { StartInfo = startInfo };
    process.Start();
    process.WaitForExit();
}