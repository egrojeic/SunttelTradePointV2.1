using System.Diagnostics;

// Ubicación del directorio del proyecto FastAPI
string rutaProyectoFastAPI = @"C:\TRADEPOINTV21\SunttelTPV2-1\APIPythonConsole\PDFAnalyse";

// Comando para activar el entorno virtual
string comandoActivarEntornoVirtual = @"C:\TRADEPOINTV21\SunttelTPV2-1\APIPythonConsole\PDFAnalyse\TPPythonEnv\Scripts\activate";

// Comando para correr el API en el entorno
string comandoCorrerAPI = @"C:\TRADEPOINTV21\SunttelTPV2-1\APIPythonConsole\PDFAnalyse\TPPythonEnv\Scripts\python.exe -m uvicorn app:app --reload --port 5167";

// Ejecutar los comandos en una terminal de comandos (CMD)
// EjecutarComandoEnCMD(rutaProyectoFastAPI, comandoActivarEntornoVirtual);

Console.WriteLine("Start API process...");

//EjecutarComandoEnCMD(rutaProyectoFastAPI, comandoCorrerAPI);

UpAndRunAssitantAPI();

Console.WriteLine("Process ran");

Console.ReadLine();

static void EjecutarComandoEnCMD(string rutaDirectorio, string comando)
{
    try
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = comando,
            UseShellExecute = false,
            WorkingDirectory = rutaDirectorio
        };


        Process process = new Process { StartInfo = startInfo };
        process.Start();
        process.WaitForExit();
    }
    catch(Exception e) {
        Console.WriteLine(e.ToString());
    }
    
}

static void UpAndRunAssitantAPI()
{
    try
    {
        // Replace "path\\to\\your\\virtualenv\\Scripts\\python.exe" with the full path to the Python interpreter
        // inside the virtual environment.
        string pythonInterpreterPath = "C:\\TRADEPOINTV21\\SunttelTPV2-1\\APIPythonConsole\\PDFAnalyse\\TPPythonEnv\\Scripts\\python";

        Environment.CurrentDirectory = "C:\\TRADEPOINTV21\\SunttelTPV2-1\\APIPythonConsole\\PDFAnalyse\\";


        // Create a ProcessStartInfo object to configure the process to run the Python script.
        ProcessStartInfo pythonProcessInfo = new ProcessStartInfo
        {
            FileName = pythonInterpreterPath,
            Arguments = "-m uvicorn app:app --host 0.0.0.0 --port 5167", // Add any additional uvicorn options here.
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // Create and start the process to run the Python script.
        using (Process pythonProcess = new Process { StartInfo = pythonProcessInfo })
        {
            pythonProcess.Start();

            // Optionally, capture and display the output from the Python script.
            string output = pythonProcess.StandardOutput.ReadToEnd();
            string error = pythonProcess.StandardError.ReadToEnd();
            Console.WriteLine("Output:");
            Console.WriteLine(output);
            Console.WriteLine("Error:");
            Console.WriteLine(error);

            // Wait for the Python script process to finish.
            pythonProcess.WaitForExit();
        }
    }
    catch (Exception ex)
    {
        // Handle any exceptions that might occur during the process execution.
        Console.WriteLine("Error: " + ex.Message);
    }
}