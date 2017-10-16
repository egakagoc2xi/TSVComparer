using System.Collections.ObjectModel;
using TSVComparer.Model;

namespace TSVComparer.ViewModel
{
    public class ChannelListViewModel : ViewModelBase
    {
        public ObservableCollection<ITSVGenericModel> ChannelInformation { get; set; }

        public ChannelListViewModel()
        {
            ChannelInformation = new ObservableCollection<ITSVGenericModel>();
        }

        public void SetChannelInformation(ObservableCollection<ITSVGenericModel> pChannelInformation)
        {
            ChannelInformation = pChannelInformation;
            RaisePropertyChanged("ChannelInformation");
        }
    }
}