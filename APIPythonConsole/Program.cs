using System.Diagnostics;

string rutaProyectoFastAPI = "PDFAnalyse";

// Comando para correr el API en el entorno
string comandoCorrerAPI = "APIPythonEnv\\Scripts\\python.exe main.py";

// Obtener la ubicación actual del código fuente
string ubicacionCodigoFuente = ObtenerRutaRaizProyecto();

// Combinar la ubicación del directorio del proyecto con la ruta relativa
string rutaCompletaProyecto = Path.Combine(ubicacionCodigoFuente, rutaProyectoFastAPI);

// Ejecutar los comandos en una terminal de comandos (CMD)
EjecutarComandoEnCMD(rutaCompletaProyecto, comandoCorrerAPI);

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

static string ObtenerRutaRaizProyecto()
{
    string ubicacionCodigoFuente = AppDomain.CurrentDomain.BaseDirectory;
    DirectoryInfo directorioActual = new DirectoryInfo(ubicacionCodigoFuente);

    // Navegar un nivel hacia arriba hasta llegar al directorio del proyecto
    while (directorioActual.Name != "APIPythonConsole")
    {
        directorioActual = directorioActual.Parent;
        if (directorioActual == null)
        {
            throw new Exception("Directorio del proyecto no encontrado.");
        }
    }

    return directorioActual.FullName;
}