using System;
using System.Collections.ObjectModel;
using TSVComparer.Model;

namespace TSVComparer.ViewModel
{
    public class ResultViewModel : ViewModelBase
    {
        public ObservableCollection<CompareChannelInformation> RemapedChannelInformation { get; set; }
        public ObservableCollection<CompareChannelInformation> RenamedChannelInformation { get; set; }
        public ObservableCollection<CompareChannelInformation> AddedChannelInformation { get; set; }
        public ObservableCollection<CompareChannelInformation> RemovedChannelInformation { get; set; }

        public ResultViewModel()
        {
            RemapedChannelInformation = new ObservableCollection<CompareChannelInformation>();
            RenamedChannelInformation = new ObservableCollection<CompareChannelInformation>();
            AddedChannelInformation = new ObservableCollection<CompareChannelInformation>();
            RemovedChannelInformation = new ObservableCollection<CompareChannelInformation>();
        }

        public void SetRemapedChannelInformation(ObservableCollection<CompareChannelInformation> pRemapedChannelInformation)
        {
            RemapedChannelInformation = pRemapedChannelInformation;
            RaisePropertyChanged("RemmapedChannelInformation");
        }

        public void SetRenamedChannelInformation(ObservableCollection<CompareChannelInformation> pRenamedChannelInformation)
        {
            RenamedChannelInformation = pRenamedChannelInformation;
            RaisePropertyChanged("RenamedChannelInformation");
        }

        public void SetAddedChannelInformation(ObservableCollection<CompareChannelInformation> pAddedChannelInformation)
        {
            AddedChannelInformation = pAddedChannelInformation;
            RaisePropertyChanged("AddedChannelInformation");
        }

        public void SetRemovedChannelInformation(ObservableCollection<CompareChannelInformation> pRemovedChannelInformation)
        {
            RemovedChannelInformation = pRemovedChannelInformation;
            RaisePropertyChanged("RemovedChannelInformation");
        }
    }
}
