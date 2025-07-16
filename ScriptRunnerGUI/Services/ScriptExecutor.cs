using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace ScriptRunnerGUI.Services
{
    public class ScriptExecutor
    {
        public string? PythonPath { get; set; }
        private readonly Dictionary<string, Func<string, (string fileName, string arguments)>> _scriptHandlers;

        // Implement usecase (can later be set via GUI)
        public void SetPythonPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                throw new ArgumentException("Invalid Python interpreter path.", nameof(path));
            }
            PythonPath = path;
        }

        public ScriptExecutor()
        {
            _scriptHandlers = new Dictionary<string, Func<string, (string fileName, string arguments)>>
            {
                [".py"]  = path => (PythonPath ?? (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "python" : "python3"), $"\"{path}\""),
                [".ps1"] = path => ("powershell", $"-ExecutionPolicy Bypass -File \"{path}\""),
                [".sh"]  = path => ("bash", $"\"{path}\""),
                [".cmd"] = path => ("cmd.exe", $"/c \"{path}\""),
                [".js"]  = path => ("node", $"\"{path}\""),
                [".bat"] = path => ("cmd.exe", $"/c \"{path}\"")
            };
        }

        public void RunScript(string scriptName)
        {
            string baseDir = AppContext.BaseDirectory;
            string scriptPath = Path.Combine(baseDir, "Scripts", scriptName);

            if (!File.Exists(scriptPath))
            {
                // Write feedback message to gui
                return;
            }
            string extension = Path.GetExtension(scriptName).ToLowerInvariant(); 

            if (!_scriptHandlers.TryGetValue(extension, out var scriptHandler))
            {
                // Write feedback message to gui
                return;
            }
            var (fileName, arguments) = scriptHandler(scriptPath);

            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                CreateNoWindow = true
            };

            try
            {
                using var process = Process.Start(startInfo);
                if (process != null)
                {
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        Console.WriteLine($"Script {scriptName} exited with code {process.ExitCode}");
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to start script: {scriptName}");
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error running script {scriptName}: {ex.Message}");
            }
        }
    }
}
