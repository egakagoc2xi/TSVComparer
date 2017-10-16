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
        public virtual void BatchProcess(CompareInformation pCompareInformation)
        {

            if (String.IsNullOrEmpty(pCompareInformation.OriginalFolder)) throw new ArgumentException();
            if (String.IsNullOrEmpty(pCompareInformation.CompareFolder)) throw new ArgumentException();
            if (String.IsNullOrEmpty(pCompareInformation.OutputFolder)) throw new ArgumentException();

            System.Console.WriteLine("/*************************************************************************/");
            Version a = Assembly.GetEntryAssembly().GetName().Version;

            System.Console.WriteLine("TSV Comparer - " + Assembly.GetEntryAssembly().GetName().Version.ToString());
            System.Console.WriteLine("Starting Comparing Batch Procces with the current Information: ");
            System.Console.WriteLine("Original Folder: " + pCompareInformation.OriginalFolder);
            System.Console.WriteLine("Compare Folder: " + pCompareInformation.CompareFolder);
            System.Console.WriteLine("Dump Folder:" + pCompareInformation.OutputFolder);
            System.Console.WriteLine("Source Type: " + pCompareInformation.TypeOfFileComparisson.ToString());
            System.Console.WriteLine("Output Type: " + pCompareInformation.TypeOfOuputFile.ToString());
            System.Console.WriteLine("/*************************************************************************/");


            ObservableCollection<ITSVGenericModel> _orginalFileInformation = this.LoadListFromDirectory(pCompareInformation.OriginalFolder, pCompareInformation.TypeOfFileComparisson);
            ObservableCollection<ITSVGenericModel> _comparissonFileInformation = this.LoadListFromDirectory(pCompareInformation.CompareFolder, pCompareInformation.TypeOfFileComparisson);

            this.CompareChannels(_orginalFileInformation, _comparissonFileInformation);

            switch (pCompareInformation.TypeOfOuputFile)
            {
                case TypeOfOutput.Excel:
                    ExcelSaveFileCommandExecute(pCompareInformation.OutputFolder, pCompareInformation.FullExcelOuput);
                    break;
                case TypeOfOutput.TSV:
                    TSVSaveFileCommandExecute(pCompareInformation.OutputFolder);
                    break;
                case TypeOfOutput.BBCode:
                    BBCodeSaveFileCommandExecute(pCompareInformation.OutputFolder);
                    break;
                case TypeOfOutput.HTML:
                    HtmlSaveFileCommandExecute(pCompareInformation.OutputFolder);
                    break;
                default:
                    break;
            }
        }
    }
}
