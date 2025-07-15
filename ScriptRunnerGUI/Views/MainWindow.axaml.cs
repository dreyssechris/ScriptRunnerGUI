using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Threading.Tasks;
using ScriptRunnerGUI.ViewModels;


namespace ScriptRunnerGUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); // creates ViewModel in the UI thread
        }
    }
}