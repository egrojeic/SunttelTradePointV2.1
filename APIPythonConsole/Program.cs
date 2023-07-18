using System.Diagnostics;

// Ubicación del directorio del proyecto FastAPI
string rutaProyectoFastAPI = @"C:\Proyectos\SunttelTradePointV2.1\APIPythonConsole\PDFAnalyse";

// Comando para activar el entorno virtual
string comandoActivarEntornoVirtual = @"mi_entorno\Scripts\activate";

// Comando para correr el API en el entorno
string comandoCorrerAPI = @"C:\Proyectos\SunttelTradePointV2.1\APIPythonConsole\PDFAnalyse\APIPythonEnv\Scripts\python.exe main.py";

// Ejecutar los comandos en una terminal de comandos (CMD)
//EjecutarComandoEnCMD(rutaProyectoFastAPI, comandoActivarEntornoVirtual);
EjecutarComandoEnCMD(rutaProyectoFastAPI, comandoCorrerAPI);

Console.WriteLine("Presiona Enter para salir...");
Console.ReadLine();

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