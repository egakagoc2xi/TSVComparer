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
using System.Xml;
using TSVComparer.Model;


namespace TSVComparer.ViewModel
{
    public abstract partial class MainWindowViewModel : ViewModelBase
    {
        public ResultViewModel ResultViewModelProperty { get; set; }
        public ChannelListViewModel OriginalChannelListViewModelProperty { get; set; }
        public ChannelListViewModel CompareChannelListViewModelProperty { get; set; }

        public Boolean? IsDssTableExplorerProperty { get; set; }
        public Boolean? IsSTBScannerProperty { get; set; }
        public Boolean? IsGCTProperty { get; set; }

        public String CompareDirectoryProperty { get; set; }
        public String OriginalDirectoryProperty { get; set; }

        public MainWindowViewModel(String pOriginalsFileDirectory, String pComparissonFilesDirectory)
        {
            LoadViewModels(pOriginalsFileDirectory, pComparissonFilesDirectory);
        }

        private void LoadViewModels(String pOriginalsFileDirectory, String pComparissonFilesDirectory)
        {
            ResultViewModelProperty = new ResultViewModel();
            OriginalChannelListViewModelProperty = new ChannelListViewModel();
            CompareChannelListViewModelProperty = new ChannelListViewModel();

            CompareDirectoryProperty = pComparissonFilesDirectory;
            OriginalDirectoryProperty = pOriginalsFileDirectory;

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

        public abstract void LoadOrginalFileCommandExecute();
        public abstract void LoadComparissonFileCommandExecute();

        protected void CompareCommandExecute()
        {
            if (string.IsNullOrWhiteSpace(CompareDirectoryProperty) || string.IsNullOrWhiteSpace(OriginalDirectoryProperty))
            {
                //this.SetLog("Must indicate the file directories");
            }
            else
            {
                ObservableCollection<ITSVGenericModel> _orginalFileInformation = this.LoadListFromDirectory(OriginalDirectoryProperty);
                ObservableCollection<ITSVGenericModel> _comparissonFileInformation = this.LoadListFromDirectory(CompareDirectoryProperty);

                OriginalChannelListViewModelProperty.SetChannelInformation(_orginalFileInformation);
                CompareChannelListViewModelProperty.SetChannelInformation(_comparissonFileInformation);

                //this.SetLog(string.Format("Original {0} Channels Loaded, Compare {1} Channels Loaded", pOriginal.Count, pCompareTo.Count));

                this.CompareChannels(_orginalFileInformation, _comparissonFileInformation);
            }

            try
            {
                UpdateConfig();
            }
            catch { }
        }

        protected void CleanCommandExecute()
        {
            ResultViewModelProperty.AddedChannelInformation.Clear();
            ResultViewModelProperty.RemapedChannelInformation.Clear();
            ResultViewModelProperty.RemovedChannelInformation.Clear();
            ResultViewModelProperty.RenamedChannelInformation.Clear();
            OriginalChannelListViewModelProperty.ChannelInformation.Clear();
            CompareChannelListViewModelProperty.ChannelInformation.Clear();
        }

        public abstract void TSVSaveFileCommandExecute();
        public abstract void ExcelSaveFileCommandExecute();
        public abstract void HtmlSaveFileCommandExecute();
        public abstract void BBCodeSaveFileCommandExecute();







        public abstract void UpdateConfig();

    }
}
