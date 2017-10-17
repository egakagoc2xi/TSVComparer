using System;
using System.Collections.Generic;
using System.Text;
using TSVComparer.ViewModel;

namespace TSVComparer.Batch.ViewModel
{
    public class ConsoleViewModel : MainWindowViewModel
    {
        public ConsoleViewModel(string pOriginalsFileDirectory, string pComparissonFilesDirectory) : base(pOriginalsFileDirectory, pComparissonFilesDirectory)
        {
        }

        public override void BBCodeSaveFileCommandExecute()
        {
            throw new NotImplementedException();
        }

        public override void ExcelSaveFileCommandExecute()
        {
            throw new NotImplementedException();
        }

        public override void HtmlSaveFileCommandExecute()
        {
            throw new NotImplementedException();
        }

        public override void LoadComparissonFileCommandExecute()
        {
            throw new NotImplementedException();
        }

        public override void LoadOrginalFileCommandExecute()
        {
            throw new NotImplementedException();
        }

        public override void TSVSaveFileCommandExecute()
        {
            throw new NotImplementedException();
        }

        public override void UpdateConfig()
        {
            throw new NotImplementedException();
        }
    }
}
