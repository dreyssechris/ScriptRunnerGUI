using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScriptRunnerGUI.Services;
using System.Threading.Tasks;

namespace ScriptRunnerGUI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ScriptExecutor _executor = new();

        // This method will be executed asynchronously when the command is invoked.
        [RelayCommand]
        public async Task RunScriptAsync()
        {
            await Task.Run(() => _executor.RunPythonScript("clean-up.py"));
        }
    }
}