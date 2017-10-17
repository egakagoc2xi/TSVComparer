using System;
using System.Reflection;
using System.Windows;
using TSVComparer.WPF.ViewModel;

namespace TSVComparer.WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel _viewModel = new MainWindowViewModel(String.Empty, String.Empty);
            usrMainWindows.DataContext = _viewModel;

            Version a = Assembly.GetEntryAssembly().GetName().Version;

            this.Title = "TSV Comparer - " + a.ToString();
        }
    }
}
