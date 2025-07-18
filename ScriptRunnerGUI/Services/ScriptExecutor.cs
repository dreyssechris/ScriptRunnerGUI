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

        // Implement usecase (can later be set via GUI)
        public void SetPythonPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                throw new ArgumentException("Invalid Python interpreter path.", nameof(path));
            }
            PythonPath = path;
        }

        public (string fileName, string arguments)? ResolveScript(string scriptName)
        {
            string baseDir = AppContext.BaseDirectory;
            string scriptPath = Path.Combine(baseDir, "Scripts", scriptName);

            if (!File.Exists(scriptPath))
                // Maybe write feedback message to GUI
                return null;

            string extension = Path.GetExtension(scriptName).ToLowerInvariant();

            // erklären lassen
            if (!_scriptHandlers.TryGetValue(extension, out var handler))
                return null;

            return handler(scriptPath);

        }
    }
}