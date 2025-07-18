using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScriptRunnerGUI.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScriptRunnerGUI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ScriptExecutor _executor = new();
        private readonly IProcessRunner _processRunner = new ProcessRunner();

        [ObservableProperty]
        private string terminalOutput; // Fix: Ensure this property is defined

        public ICommand RunScriptCleanUpCommand { get; }
        public ICommand RunScriptAddVSFunctionCommand { get; }

        public MainWindowViewModel()
        {
            RunScriptCleanUpCommand = new RelayCommand(async () => await RunScript("clean-up.py"));
            RunScriptAddVSFunctionCommand = new RelayCommand(async () => await RunScript("Add-VSFunction.ps1"));
            TerminalOutput = string.Empty;
        }

        private async Task RunScript(string scriptName)
        {
            var script = _executor.ResolveScript(scriptName);

            if (script is null)
            {
                TerminalOutput += $"[Error] Script not found: {scriptName}\n";
                return;
            }

            TerminalOutput += $"[Running] {script.Value.fileName} {script.Value.arguments}\n";

            await _processRunner.RunAsync(script.Value.fileName, script.Value.arguments,
                output => TerminalOutput += $"[Output] {output}\n",
                error => TerminalOutput += $"[Error] {error}\n",
                exitCode => TerminalOutput += $"[Exit Code] {exitCode}\n");
        }
    }
}