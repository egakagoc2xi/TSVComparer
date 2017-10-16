using System;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace TSVComparer.Model
{
    public class CompareChannelInformation
    {
        private string _channelName;
        private string _PreviousChannelName;
        private string _channelNumber;
        private int _network;
        private int _networkDestination;
        private CompareResultOptions _result;
        private int _tid;
        private int _tidDestination;
        private int _transponder;
        private int _transponderDestination;
        private string _vPid;
        private string _vPidDestination;
        public int MajorNumber;
        public int MinorNumber;
        private string _Market;
        private string _satellite;
        private string _satelliteDestination;

        public CompareChannelInformation(ITSVGenericModel pDestination, ITSVGenericModel pOriginal, CompareResultOptions pResult)
        {
            char[] chArray;
            string[] strArray;
            this._result = pResult;
            if (pResult == CompareResultOptions.Added || pResult == CompareResultOptions.Renamed)
            {
                this._networkDestination = pDestination.Network;
                this._transponderDestination = pDestination.Transponder;
                this._vPidDestination = pDestination.VPid;
                this._tidDestination = pDestination.Tid;
                this._channelName = pDestination.ChannelName;
                this._channelNumber = pDestination.ChannelNumber;
                this._Market = pDestination.Market;
                this._satelliteDestination = pDestination.Satellite;
                if (pResult == CompareResultOptions.Renamed) this._PreviousChannelName = pOriginal.ChannelName;
            }
            else if (pResult == CompareResultOptions.Removed)
            {
                this._network = pDestination.Network;
                this._transponder = pDestination.Transponder;
                this._vPid = pDestination.VPid;
                this._tid = pDestination.Tid;
                this._channelName = pDestination.ChannelName;
                this._channelNumber = pDestination.ChannelNumber;
                this._Market = pDestination.Market;
                this._satellite = pDestination.Satellite;
            }
            else
            {
                this._networkDestination = pDestination.Network;
                this._transponderDestination = pDestination.Transponder;
                this._vPidDestination = pDestination.VPid;
                this._tidDestination = pDestination.Tid;
                this._channelName = pDestination.ChannelName;
                this._channelNumber = pDestination.ChannelNumber;
                this._network = pOriginal.Network;
                this._transponder = pOriginal.Transponder;
                this._vPid = pOriginal.VPid;
                this._tid = pOriginal.Tid;
                this._Market = pDestination.Market;
                this._satelliteDestination = pDestination.Satellite;
                this._satellite = pOriginal.Satellite;
            }
            if (this._channelNumber.Trim().Contains("-"))
            {
                this.MinorNumber = 0;
                chArray = new char[] { Convert.ToChar("-") };
                strArray = this._channelNumber.Split(chArray, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == 2)
                {
                    if (!int.TryParse(strArray[0].Trim(), out this.MajorNumber))
                    {
                        this.MajorNumber = -1;
                    }
                    if (!int.TryParse(strArray[1].Trim(), out this.MinorNumber))
                    {
                        this.MinorNumber = 0;
                    }
                }
                else if (strArray.Length == 1)
                {
                    if (!int.TryParse(strArray[0].Trim(), out this.MajorNumber))
                    {
                        this.MajorNumber = -1;
                    }
                    else
                    {
                        this.MajorNumber = -1;
                    }
                }
            }
            else if (this._channelNumber.Trim().Contains("."))
            {
                this.MinorNumber = 0;
                chArray = new char[] { Convert.ToChar(".") };
                strArray = this._channelNumber.Split(chArray, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == 2)
                {
                    if (!int.TryParse(strArray[0].Trim(), out this.MajorNumber))
                    {
                        this.MajorNumber = -1;
                    }
                    if (!int.TryParse(strArray[1].Trim(), out this.MinorNumber))
                    {
                        this.MinorNumber = 0;
                    }
                }
                else if (strArray.Length == 1)
                {
                    if (!int.TryParse(strArray[0].Trim(), out this.MajorNumber))
                    {
                        this.MajorNumber = -1;
                    }
                    else
                    {
                        this.MajorNumber = -1;
                    }
                }
            }
            else
            {
                this.MinorNumber = 0;
                if (!int.TryParse(this._channelNumber.Trim(), out this.MajorNumber))
                {
                    this.MajorNumber = -1;
                }
            }
        }

        public string ToBBCodeString()
        {
            var bbCodeString = "[size=2][color=#000000]";
            bbCodeString += "[color=#000000]" + _channelNumber.ToString() + "[/color]" + EmptySpacesBBC(15 - _channelNumber.ToString().Length);
            bbCodeString += "[color=#000000]" + _channelName.ToString() + "[/color]" + EmptySpacesBBC(15 - _channelName.ToString().Length);
            if (_result == CompareResultOptions.Renamed)
                bbCodeString += "[color=#000000]" + _PreviousChannelName.ToString() + "[/color]" + EmptySpacesBBC(15 - _PreviousChannelName.ToString().Length);

            bbCodeString += "[color=#000000]" + _Market.ToString() + "[/color]" + EmptySpacesBBC(15 - _Market.ToString().Length);

            if (_result == CompareResultOptions.Removed)
            {

                bbCodeString += "[color=#000000]" + _network.ToString() + "[/color]" + EmptySpacesBBC(15 - _network.ToString().Length);
                bbCodeString += "[color=#000000]" + _tid.ToString() + "[/color]" + EmptySpacesBBC(15 - _tid.ToString().Length);
                bbCodeString += "[color=#000000]" + _transponder.ToString() + "[/color]" + EmptySpacesBBC(15 - _transponder.ToString().Length);
                bbCodeString += "[color=#000000]" + _vPid.ToString() + "[/color]" + EmptySpacesBBC(15 - _vPid.ToString().Length);
            }
            else
            {
                bbCodeString += "[color=#000000]" + _networkDestination.ToString() + "[/color]" + EmptySpacesBBC(15 - _networkDestination.ToString().Length);
                bbCodeString += "[color=#000000]" + _tidDestination.ToString() + "[/color]" + EmptySpacesBBC(15 - _tidDestination.ToString().Length);
                bbCodeString += "[color=#000000]" + _transponderDestination.ToString() + "[/color]" + EmptySpacesBBC(15 - _transponderDestination.ToString().Length);
                bbCodeString += "[color=#000000]" + _vPidDestination.ToString() + "[/color]" + EmptySpacesBBC(15 - _vPidDestination.ToString().Length);
            }

            if (_result == CompareResultOptions.Remapped)
            {
                bbCodeString += "[color=#000000]<--------- [/color]" + EmptySpacesBBC(4);
                bbCodeString += "[color=#000000]" + _network.ToString() + "[/color]" + EmptySpacesBBC(15 - _network.ToString().Length);
                bbCodeString += "[color=#000000]" + _tid.ToString() + "[/color]" + EmptySpacesBBC(15 - _tid.ToString().Length);
                bbCodeString += "[color=#000000]" + _transponder.ToString() + "[/color]" + EmptySpacesBBC(15 - _transponder.ToString().Length);
                bbCodeString += "[color=#000000]" + _vPid.ToString() + "[/color]" + EmptySpacesBBC(15 - _vPid.ToString().Length);
            }

            return bbCodeString += "[/size][/color]";
        }

        public string ToHtmlString()
        {
            var htmlCodeString = "<tr>";
            htmlCodeString += "<td>" + _channelNumber.ToString();
            htmlCodeString += "</td><td>" + _channelName.ToString();
            if (_result == CompareResultOptions.Renamed)
                htmlCodeString += "</td><td>" + _PreviousChannelName.ToString();
            htmlCodeString += "</td><td>" + _Market.ToString();

            if (_result == CompareResultOptions.Removed)
            {
                htmlCodeString += "</td><td>" + _network.ToString();
                htmlCodeString += "</td><td>" + _tid.ToString();
                htmlCodeString += "</td><td>" + _transponder.ToString();
                htmlCodeString += "</td><td>" + _vPid.ToString();
            }
            else
            {
                htmlCodeString += "</td><td>" + _networkDestination.ToString();
                htmlCodeString += "</td><td>" + _tidDestination.ToString();
                htmlCodeString += "</td><td>" + _transponderDestination.ToString();
                htmlCodeString += "</td><td>" + _vPidDestination.ToString();
            }

            if (_result == CompareResultOptions.Remapped)
            {
                htmlCodeString += "</td><td><---------<td>";
                htmlCodeString += "<td>" + _network.ToString();
                htmlCodeString += "</td><td>" + _tid.ToString();
                htmlCodeString += "</td><td>" + _transponder.ToString();
                htmlCodeString += "</td><td>" + _vPid.ToString();
            }

            return htmlCodeString += "</td><tr>";
        }

        public override string ToString()
        {
            switch (this._result)
            {
                case CompareResultOptions.Removed:
                    return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", new object[] { this._channelNumber, this._channelName, String.Empty, this.Market, this._network, this._tid, this._transponder, this._vPid, "-" });

                case CompareResultOptions.Added:
                    return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", new object[] { this._channelNumber, this._channelName, String.Empty, this.Market, this._networkDestination, this._tidDestination, this._transponderDestination, this._vPidDestination, "+" });

                case CompareResultOptions.Remapped:
                    return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}", new object[] { this._channelNumber, this._channelName, String.Empty, this.Market, this._networkDestination, this._tidDestination, this._transponderDestination, this._vPidDestination, "<------", this._network, this._tid, this._transponder, this._vPid });

                case CompareResultOptions.Renamed:
                    return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", new object[] { this._channelNumber, this._channelName, this._PreviousChannelName, this.Market, this._networkDestination, this._tidDestination, this._transponderDestination, this._vPidDestination, "*" });
            }
            return string.Empty;
        }

        public void ToExcelRow(ExcelWorksheet ws, int rowNumber)
        {
            switch (this.Result)
            {
                case CompareResultOptions.Removed:
                    ws.SetValue(rowNumber, 1, this.ChannelNumber);
                    ws.SetValue(rowNumber, 2, this.ChannelName);
                    ws.SetValue(rowNumber, 3, this.Market);

                    ws.SetValue(rowNumber, 10, this.NetworkOriginal);
                    ws.SetValue(rowNumber, 11, this.SatelliteOriginal);
                    ws.SetValue(rowNumber, 12, this.TidOriginal);
                    ws.SetValue(rowNumber, 13, this.TransponderOriginal);
                    ws.SetValue(rowNumber, 14, this.VPidOriginal);
                    break;
                case CompareResultOptions.Added:
                    ws.SetValue(rowNumber, 1, this.ChannelNumber);
                    ws.SetValue(rowNumber, 2, this.ChannelName);
                    ws.SetValue(rowNumber, 3, this.Market);
                    ws.SetValue(rowNumber, 4, this.NetworkDestination);
                    ws.SetValue(rowNumber, 5, this.SatelliteDestination);
                    ws.SetValue(rowNumber, 6, this.TidDestination);
                    ws.SetValue(rowNumber, 7, this.TransponderDestination);
                    ws.SetValue(rowNumber, 8, this.VPidDestination);
                    break;
                case CompareResultOptions.Remapped:
                    ws.SetValue(rowNumber, 1, this.ChannelNumber);
                    ws.SetValue(rowNumber, 2, this.ChannelName);
                    ws.SetValue(rowNumber, 3, this.Market);
                    ws.SetValue(rowNumber, 4, this.NetworkDestination);
                    ws.SetValue(rowNumber, 5, this.SatelliteDestination);
                    ws.SetValue(rowNumber, 6, this.TidDestination);
                    ws.SetValue(rowNumber, 7, this.TransponderDestination);
                    ws.SetValue(rowNumber, 8, this.VPidDestination);

                    ws.SetValue(rowNumber, 10, this.NetworkOriginal);
                    ws.SetValue(rowNumber, 11, this.SatelliteOriginal);
                    ws.SetValue(rowNumber, 12, this.TidOriginal);
                    ws.SetValue(rowNumber, 13, this.TransponderOriginal);
                    ws.SetValue(rowNumber, 14, this.VPidOriginal);
                    break;
                case CompareResultOptions.Renamed:
                    ws.SetValue(rowNumber, 1, this.ChannelNumber);
                    ws.SetValue(rowNumber, 2, this.ChannelName);
                    ws.SetValue(rowNumber, 3, this.Market);
                    ws.SetValue(rowNumber, 4, this.NetworkDestination);
                    ws.SetValue(rowNumber, 5, this.SatelliteDestination);
                    ws.SetValue(rowNumber, 6, this.TidDestination);
                    ws.SetValue(rowNumber, 7, this.TransponderDestination);
                    ws.SetValue(rowNumber, 8, this.VPidDestination);
                    ws.SetValue(rowNumber, 9, this.PreviousChannelName);
                    break;
                default:
                    break;
            }

            ws.Cells[rowNumber, 4, rowNumber, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[rowNumber, 4, rowNumber, 8].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

            ws.Cells[rowNumber, 9, rowNumber, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[rowNumber, 9, rowNumber, 14].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
        }

        private String EmptySpacesBBC(int pSpaces)
        {
            String response = "[color=#ffffff]";

            for (int i = 0; i < pSpaces; i++)
                response += "O";

            response += "[/color]";

            return response;
        }

        public string ChannelName
        {
            get
            {
                return this._channelName;
            }
        }

        public string PreviousChannelName
        {
            get
            {
                return this._PreviousChannelName;
            }
        }

        public string ChannelNumber
        {
            get
            {
                return this._channelNumber;
            }
        }

        public String Market
        {
            get
            {
                return String.IsNullOrEmpty(this._Market) ? String.Empty : this._Market;
            }
        }

        public int NetworkDestination
        {
            get
            {
                return this._networkDestination;
            }
        }

        public int NetworkOriginal
        {
            get
            {
                return this._network;
            }
        }

        public CompareResultOptions Result
        {
            get
            {
                return this._result;
            }
        }

        public int TidDestination
        {
            get
            {
                return this._tidDestination;
            }
        }

        public int TidOriginal
        {
            get
            {
                return this._tid;
            }
        }

        public int TransponderDestination
        {
            get
            {
                return this._transponderDestination;
            }
        }

        public int TransponderOriginal
        {
            get
            {
                return this._transponder;
            }
        }

        public string VPidDestination
        {
            get
            {
                return this._vPidDestination;
            }
        }

        public string VPidOriginal
        {
            get
            {
                return this._vPid;
            }
        }

        public string SatelliteOriginal
        {
            get
            {
                return this._satellite;
            }
        }

        public string SatelliteDestination
        {
            get
            {
                return this._satelliteDestination;
            }
        }
    }
}