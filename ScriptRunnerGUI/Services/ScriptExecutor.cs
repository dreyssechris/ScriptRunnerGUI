using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace ScriptRunnerGUI.Services
{
    public class ScriptExecutor
    {
        public string? PythonPath { get; set; }

        public void RunPythonScript(string scriptName)
        {
            string baseDir = AppContext.BaseDirectory;
            string scriptPath = Path.Combine(baseDir, "Scripts", scriptName);
            string interpreter = PythonPath ?? (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "python" : "python3");

            if (!File.Exists(scriptPath))
                return;

            var startInfo = new ProcessStartInfo
            {
                FileName = interpreter,
                Arguments = $"\"{scriptPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo);
            process?.WaitForExit();
        }
    }
}
