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
        private ObservableCollection<ITSVGenericModel> LoadListFromFile(string pPath)
        {
            ObservableCollection<ITSVGenericModel> list = new ObservableCollection<ITSVGenericModel>();

            try
            {
                string str3 = File.OpenText(pPath).ReadToEnd();
                char[] separator = new char[] { Convert.ToChar(13) };
                string[] strArray2 = str3.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                //this.SetLog(string.Format("Loading {0} from File {1}......", strArray2.Length, str2));
                for (int i = 0; i < strArray2.Length; i++)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(strArray2[i].Trim()))
                        {
                            if (IsDssTableExplorerProperty.GetValueOrDefault())
                            {
                                list.Add(new DSSTableExplorerChannelInformation(strArray2[i]));
                            }
                            else if (IsSTBScannerProperty.GetValueOrDefault())
                            {
                                list.Add(new STBScannerChannelInformation(strArray2[i]));
                            }
                            else if (IsGCTProperty.GetValueOrDefault())
                            {
                                list.Add(new GCTChannelInformation(strArray2[i]));
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        //this.SetLog(string.Format("Error loading File {0} at Line {1}. Error Message: {2}", str2, i + 1, exception.Message));
                    }
                }
                return new ObservableCollection<ITSVGenericModel>(list.Distinct<ITSVGenericModel>(new FullComparisson()).OrderBy(p => p.MinorNumber).OrderBy(p => p.MajorNumber));
            }
            catch (Exception exception2)
            {
                //this.SetLog(string.Format("Error cargando el archivo {1}: {0}", exception2.Message, pPath));
                return list;
            }
        }

        private ObservableCollection<ITSVGenericModel> LoadListFromFile(string pPath, TypeOfFile pTypeOfFile)
        {
            ObservableCollection<ITSVGenericModel> list = new ObservableCollection<ITSVGenericModel>();

            try
            {
                string str3 = File.OpenText(pPath).ReadToEnd();
                char[] separator = new char[] { Convert.ToChar(13) };
                string[] strArray2 = str3.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                //this.SetLog(string.Format("Loading {0} from File {1}......", strArray2.Length, str2));
                for (int i = 0; i < strArray2.Length; i++)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(strArray2[i].Trim()))
                        {
                            switch (pTypeOfFile)
                            {
                                case TypeOfFile.DSSTE:
                                    list.Add(new DSSTableExplorerChannelInformation(strArray2[i]));
                                    break;
                                case TypeOfFile.STB:
                                    list.Add(new STBScannerChannelInformation(strArray2[i]));
                                    break;
                                case TypeOfFile.GCT:
                                    list.Add(new GCTChannelInformation(strArray2[i]));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        //this.SetLog(string.Format("Error loading File {0} at Line {1}. Error Message: {2}", str2, i + 1, exception.Message));
                    }
                }
                return new ObservableCollection<ITSVGenericModel>(list.Distinct<ITSVGenericModel>(new FullComparisson()).OrderBy(p => p.MinorNumber).OrderBy(p => p.MajorNumber));
            }
            catch (Exception exception2)
            {
                //this.SetLog(string.Format("Error cargando el archivo {1}: {0}", exception2.Message, pPath));
                return list;
            }
        }

        private ObservableCollection<ITSVGenericModel> LoadListFromDirectory(string pPath)
        {
            ObservableCollection<ITSVGenericModel> list = new ObservableCollection<ITSVGenericModel>();

            List<string> list2 = new List<string>();
            list2.AddRange(Directory.GetDirectories(pPath));

            foreach (var item in list2)
            {
                list = new ObservableCollection<ITSVGenericModel>(list.Union(LoadListFromDirectory(item)));
            }

            list2.Clear();
            string[] strArray = new string[] { "*.tsv", "*.txt" };
            foreach (string str in strArray)
            {
                list2.AddRange(Directory.GetFiles(pPath, str));
            }

            foreach (var item in list2)
            {
                list = new ObservableCollection<ITSVGenericModel>(list.Union(LoadListFromFile(item)));
            }

            return new ObservableCollection<ITSVGenericModel>(list.Distinct<ITSVGenericModel>(new FullComparisson()).OrderBy(p => p.MinorNumber).OrderBy(p => p.MajorNumber));
        }

        private ObservableCollection<ITSVGenericModel> LoadListFromDirectory(string pPath, TypeOfFile pTypeOfFile)
        {
            ObservableCollection<ITSVGenericModel> list = new ObservableCollection<ITSVGenericModel>();

            List<string> list2 = new List<string>();
            list2.AddRange(Directory.GetDirectories(pPath));

            foreach (var item in list2)
            {
                list = new ObservableCollection<ITSVGenericModel>(list.Union(LoadListFromDirectory(item, pTypeOfFile)));
            }

            list2.Clear();
            string[] strArray = new string[] { "*.tsv", "*.txt" };
            foreach (string str in strArray)
            {
                list2.AddRange(Directory.GetFiles(pPath, str));
            }

            foreach (var item in list2)
            {
                list = new ObservableCollection<ITSVGenericModel>(list.Union(LoadListFromFile(item, pTypeOfFile)));
            }

            return new ObservableCollection<ITSVGenericModel>(list.Distinct<ITSVGenericModel>(new FullComparisson()).OrderBy(p => p.MinorNumber).OrderBy(p => p.MajorNumber));
        }

        private void CompareChannels(ObservableCollection<ITSVGenericModel> pOriginal, ObservableCollection<ITSVGenericModel> pCompareTo)
        {
            IList<CompareChannelInformation> list = new List<CompareChannelInformation>();

            IList<ITSVGenericModel> first = (from p in pOriginal.Except<ITSVGenericModel>(pCompareTo, new FullComparisson())
                                             orderby p.MajorNumber
                                             orderby p.MinorNumber
                                             orderby p.ChannelNumber
                                             orderby p.ChannelName
                                             select p).ToList<ITSVGenericModel>();

            IList<ITSVGenericModel> second = (from p in pCompareTo.Except<ITSVGenericModel>(pOriginal, new FullComparisson())
                                              orderby p.MajorNumber
                                              orderby p.MinorNumber
                                              orderby p.ChannelNumber
                                              orderby p.ChannelName
                                              select p).ToList<ITSVGenericModel>();

            IList<ITSVGenericModel> list5 = (from p in first.Intersect<ITSVGenericModel>(second, new ChannelComparisonRename())
                                             orderby p.MajorNumber
                                             orderby p.MinorNumber
                                             orderby p.ChannelNumber
                                             orderby p.Network
                                             orderby p.Transponder
                                             orderby p.VPid
                                             select p).ToList<ITSVGenericModel>();

            IList<ITSVGenericModel> list6 = (from p in second.Intersect<ITSVGenericModel>(first, new ChannelComparisonRename())
                                             orderby p.MajorNumber
                                             orderby p.MinorNumber
                                             orderby p.ChannelNumber
                                             orderby p.Network
                                             orderby p.Transponder
                                             orderby p.VPid
                                             select p).ToList<ITSVGenericModel>();

            for (int i = 0; i < list6.Count; i++)
            {
                list.Add(new CompareChannelInformation(list5[i], list6[i], CompareResultOptions.Renamed));
            }

            IList<ITSVGenericModel> list4 = (from p in first.Except<ITSVGenericModel>(list6, new ChannelComparisonRename())
                                             orderby p.MajorNumber
                                             orderby p.MinorNumber
                                             orderby p.ChannelNumber
                                             orderby p.ChannelName
                                             select p).ToList<ITSVGenericModel>();

            second = (from p in second.Except<ITSVGenericModel>(list5, new ChannelComparisonRename())
                      orderby p.MajorNumber
                      orderby p.MinorNumber
                      orderby p.ChannelNumber
                      orderby p.ChannelName
                      select p).ToList<ITSVGenericModel>();

            first = list4;

            foreach (ITSVGenericModel model2 in first.Except<ITSVGenericModel>(second, new ChannelComparison()))
            {
                list.Add(new CompareChannelInformation(model2, null, CompareResultOptions.Added));
            }

            foreach (ITSVGenericModel model3 in second.Except<ITSVGenericModel>(first, new ChannelComparison()))
            {
                list.Add(new CompareChannelInformation(model3, null, CompareResultOptions.Removed));
            }

            list4 = (from p in first.Intersect<ITSVGenericModel>(second, new ChannelComparison())
                     orderby p.MajorNumber
                     orderby p.MinorNumber
                     orderby p.ChannelNumber
                     orderby p.ChannelName
                     select p).ToList<ITSVGenericModel>();

            second = (from p in second.Intersect<ITSVGenericModel>(first, new ChannelComparison())
                      orderby p.MajorNumber
                      orderby p.MinorNumber
                      orderby p.ChannelNumber
                      orderby p.ChannelName
                      select p).ToList<ITSVGenericModel>();

            first = list4;

            for (int i = 0; i < second.Count; i++)
            {
                list.Add(new CompareChannelInformation(first[i], second[i], CompareResultOptions.Remapped));
            }

            list = new ObservableCollection<CompareChannelInformation>(list.OrderBy(p => p.MinorNumber).OrderBy(p => p.MajorNumber));

            ResultViewModelProperty.RemapedChannelInformation.Clear();
            ResultViewModelProperty.RenamedChannelInformation.Clear();
            ResultViewModelProperty.RemovedChannelInformation.Clear();
            ResultViewModelProperty.AddedChannelInformation.Clear();

            foreach (var item in list)
            {
                switch (item.Result)
                {
                    case CompareResultOptions.Removed:
                        ResultViewModelProperty.RemovedChannelInformation.Add(item);
                        break;
                    case CompareResultOptions.Added:
                        ResultViewModelProperty.AddedChannelInformation.Add(item);
                        break;
                    case CompareResultOptions.Remapped:
                        ResultViewModelProperty.RemapedChannelInformation.Add(item);
                        break;
                    case CompareResultOptions.Renamed:
                        ResultViewModelProperty.RenamedChannelInformation.Add(item);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
