using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunnerGUI.Services
{
    internal class ProcessRunner : IProcessRunner
    {
        public async Task RunAsync(string fileName, string arguments, Action<string>? onOutput = null, Action<string>? onError = null, Action<int>? onExit = null)
        {
            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.OutputDataReceived += (s, e) => { if (e.Data != null) onOutput?.Invoke(e.Data); };
                process.ErrorDataReceived += (s, e) => { if (e.Data != null) onError?.Invoke(e.Data); };
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                await process.WaitForExitAsync();
                onExit?.Invoke(process.ExitCode);
            }
            catch (Exception ex)
            {
                onError?.Invoke($"[Error] {ex.Message}");
                onExit?.Invoke(-1);
            }
        }
    }
}
