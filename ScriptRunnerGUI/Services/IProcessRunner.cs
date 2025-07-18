using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunnerGUI.Services
{
    public interface IProcessRunner
    {
        Task RunAsync(string fileName, string arguments, Action<string>? onOutput = null, Action<string>? onError = null, Action<int>? onExit = null);
    }
}
