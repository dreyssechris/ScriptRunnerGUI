using System.Collections.ObjectModel;

namespace ScriptRunnerGUI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia!";

        //public MainWindowViewModel()
        //{
        //    // Initialization logic can go here if needed
        //}

        public ObservableCollection<string> ScriptList { get; } = new ObservableCollection<string>()
        {
            "Script_1",
            "Script_2", 
            "Script_3", 
        }; 
    }
}
