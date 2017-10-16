using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using TSVComparer.ViewModel;
using TSVComparer.WPF.Properties;

namespace TSVComparer.WPF.ViewModel
{
    public class MainWindowViewModel : TSVComparer.ViewModel.MainWindowViewModel
    {
        public MainWindowViewModel(string pOriginalsFileDirectory, string pComparissonFilesDirectory) : base(pOriginalsFileDirectory, pComparissonFilesDirectory)
        {
            LoadViewModels();


        }

        private void LoadViewModels()
        {
            ResultViewModelProperty = new ResultViewModel();
            OriginalChannelListViewModelProperty = new ChannelListViewModel();
            CompareChannelListViewModelProperty = new ChannelListViewModel();

            CompareDirectoryProperty = Settings.Default.ComparissonFilesDirectory;
            OriginalDirectoryProperty = Settings.Default.OriginalsFileDirectory;

            IsDssTableExplorerProperty = true;
            IsSTBScannerProperty = false;
            IsGCTProperty = false;

            RaisePropertyChanged("ResultViewModelProperty");
            RaisePropertyChanged("OriginalChannelListViewModelProperty");
            RaisePropertyChanged("CompareChannelListViewModelProperty");

            RaisePropertyChanged("CompareDirectoryProperty");
            RaisePropertyChanged("OriginalDirectoryProperty");

            RaisePropertyChanged("IsDssTableExplorerProperty");
            RaisePropertyChanged("IsSTBScannerProperty");
            RaisePropertyChanged("IsGCTProperty");
        }

        public ICommand LoadOrginalFileCommand
        {
            get
            {
                return new RelayCommand(LoadOrginalFileCommandExecute);
            }
        }

        public ICommand LoadComparissonFileCommand
        {
            get
            {
                return new RelayCommand(LoadComparissonFileCommandExecute);
            }
        }

        public ICommand CompareCommand
        {
            get
            {
                return new RelayCommand(CompareCommandExecute);
            }
        }

        public ICommand CleanCommand
        {
            get
            {
                return new RelayCommand(CleanCommandExecute);
            }
        }

        public ICommand TSVSaveFileCommand
        {
            get
            {
                return new RelayCommand(TSVSaveFileCommandExecute);
            }
        }

        public ICommand BBCodeSaveFileCommand
        {
            get
            {
                return new RelayCommand(BBCodeSaveFileCommandExecute);
            }
        }

        public ICommand HtmlSaveFileCommand
        {
            get
            {
                return new RelayCommand(HtmlSaveFileCommandExecute);
            }
        }

        public ICommand ExcelSaveFileCommand
        {
            get
            {
                return new RelayCommand(ExcelSaveFileCommandExecute);
            }
        }

        public override void LoadOrginalFileCommandExecute()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = OriginalDirectoryProperty;

            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                OriginalDirectoryProperty = folderDialog.SelectedPath + "\\";
                RaisePropertyChanged("OriginalDirectoryProperty");
            }

        }

        public override void LoadComparissonFileCommandExecute()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = CompareDirectoryProperty;

            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                CompareDirectoryProperty = folderDialog.SelectedPath + "\\";
                RaisePropertyChanged("CompareDirectoryProperty");

            }
        }

        public override void TSVSaveFileCommandExecute() { TSVSaveFileCommandExecute(Settings.Default.FileDumpDirectory); }
        public override void ExcelSaveFileCommandExecute() { ExcelSaveFileCommandExecute(Settings.Default.FileDumpDirectory, Settings.Default.OutputFullExcelFile); }
        public override void HtmlSaveFileCommandExecute() { HtmlSaveFileCommandExecute(Settings.Default.FileDumpDirectory); }
        public override void BBCodeSaveFileCommandExecute() { BBCodeSaveFileCommandExecute(Settings.Default.FileDumpDirectory); }

        public override void UpdateConfig()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            
            XmlNode nodeOriginal = xml.SelectSingleNode("configuration/applicationSettings/TSVComparer.WPF.Properties.Settings/setting[@name='OriginalsFileDirectory']");
            nodeOriginal.ChildNodes[0].InnerText = OriginalDirectoryProperty;

            XmlNode nodeCompare = xml.SelectSingleNode("configuration/applicationSettings/TSVComparer.WPF.Properties.Settings/setting[@name='ComparissonFilesDirectory']");
            nodeCompare.ChildNodes[0].InnerText = CompareDirectoryProperty;

            xml.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }
    }
}
