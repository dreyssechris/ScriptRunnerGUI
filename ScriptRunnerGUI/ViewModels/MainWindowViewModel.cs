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
        public ICommand RunScriptCleanUpCommand { get; }
        public ICommand RunScriptAddVSFunctionCommand { get; }

        public MainWindowViewModel()
        {
            RunScriptCleanUpCommand       = new RelayCommand(async () => await RunScriptCleanUp());
            RunScriptAddVSFunctionCommand = new RelayCommand(async () => await RunScriptAddVSFunction());  
        }

        public async Task RunScriptCleanUp()
        {
            await Task.Run(() => _executor.RunScript("clean-up.py"));
        }
        public async Task RunScriptAddVSFunction()
        {
            await Task.Run(() => _executor.RunScript("Add-VSFunction.ps1"));
        }
    }
}