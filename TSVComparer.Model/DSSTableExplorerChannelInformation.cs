using System;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace TSVComparer.Model
{
    public class DSSTableExplorerChannelInformation : ITSVGenericModel
    {
        private string _channelName;
        private string _channelNumber;
        private string _longChannelName;
        private string _market;
        private int _network;
        private int _redirectNetwork;
        private string _secondaryPid;
        private string _shortChannelName;
        private int _tid;
        private int _transponder;
        private string _vPid;

        public DSSTableExplorerChannelInformation(string pData)
        {
            char[] separator = new char[] { Convert.ToChar(9) };
            string[] strArray = pData.Split(separator);
            if (strArray.Length < 11 || String.IsNullOrEmpty(strArray[1].Trim()))
            {
                throw new ArgumentException("Invalid DSSTableExplorer Data");
            }
            if (strArray[1].Trim().Contains("."))
            {
                base.MinorNumber = 0;
                char[] chArray2 = new char[] { Convert.ToChar(".") };
                string[] strArray2 = strArray[1].Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                if (strArray2.Length == 2)
                {
                    if (!int.TryParse(strArray2[0].Trim(), out this.MajorNumber))
                    {
                        base.MajorNumber = -1;
                    }
                    if (!int.TryParse(strArray2[1].Trim(), out this.MinorNumber))
                    {
                        base.MinorNumber = 0;
                    }
                }
                else if (strArray2.Length == 1)
                {
                    if (!int.TryParse(strArray2[0].Trim(), out this.MajorNumber))
                    {
                        base.MajorNumber = -1;
                    }
                    else
                    {
                        base.MajorNumber = -1;
                    }
                }
            }
            else
            {
                base.MinorNumber = 0;
                if (!int.TryParse(strArray[1].Trim(), out this.MajorNumber))
                {
                    base.MajorNumber = -1;
                }
            }
            this._channelName = strArray[0].Trim();
            this._channelNumber = strArray[1].Trim();
            this._network = (string.IsNullOrWhiteSpace(strArray[2].Trim()) || strArray[2].Trim().Equals("_")) ? -1 : Convert.ToInt32(strArray[2].Trim());
            this._transponder = (string.IsNullOrWhiteSpace(strArray[3].Trim()) || strArray[3].Trim().Equals("_")) ? -1 : Convert.ToInt32(strArray[3].Trim());
            this._tid = (string.IsNullOrWhiteSpace(strArray[4].Trim()) || strArray[4].Trim().Equals("_")) ? -1 : Convert.ToInt32(strArray[4].Trim());
            this._vPid = strArray[5].Trim();
            this._secondaryPid = strArray[6].Trim();
            this._redirectNetwork = (string.IsNullOrWhiteSpace(strArray[7].Trim()) || strArray[7].Trim().Equals("_")) ? -1 : Convert.ToInt32(strArray[7].Trim());
            this._market = strArray[8].Trim();
            this._shortChannelName = strArray[9].Trim();
            this._longChannelName = strArray[10].Trim();
        }

        public override string ChannelName
        {
            get
            {
                return this._channelName;
            }
        }

        public override string ChannelNumber
        {
            get
            {
                return this._channelNumber;
            }
        }

        public override string LongChannelName
        {
            get
            {
                return this._longChannelName;
            }
        }

        public override string Market
        {
            get
            {
                return this._market;
            }
        }

        public override int Network
        {
            get
            {
                if (this._redirectNetwork == -1)
                    return this._network;
                else
                    return this._redirectNetwork;
            }
        }

        public override string Satellite
        {
            get
            {
                return String.Empty;
            }
        }

        public string SecondaryPid
        {
            get
            {
                return this._secondaryPid;
            }
        }

        public override string ShortChannelName
        {
            get
            {
                return this._shortChannelName;
            }
        }

        public override int Tid
        {
            get
            {
                return this._tid;
            }
        }

        public override int Transponder
        {
            get
            {
                return this._transponder;
            }
        }

        public override string VPid
        {
            get
            {
                if (this._vPid.Trim().Equals(String.Empty))
                {
                    if (this._network > 40000)
                    {
                        return "Hybrid";
                    }
                    else return String.Empty;
                }
                if (this._vPid.Equals("1250"))
                {
                    return "PR Offline";
                }
                else if (this._vPid.Equals("1700"))
                {
                    return "Ka Offline";
                }
                else if (Convert.ToInt32(this._vPid, 16) >= 6144 && Convert.ToInt32(this._vPid, 16) <= 6297)
                {
                    return "Ka/Ku Push";
                }
                else if (this._vPid.Equals("0320") || this._vPid.Equals("320"))
                {
                    return "Ku Push";
                }
                else if (this._vPid.Equals("03AC") || this._vPid.Equals("0222") || this._vPid.Equals("222"))
                {
                    return "Ku Offline";
                }
                else
                {
                    return this._vPid;
                }
            }
        }

        public override void ToExcelRow(ExcelWorksheet ws, int rowNumber)
        {
            ws.SetValue(rowNumber, 1, this.ChannelNumber);
            ws.SetValue(rowNumber, 2, this.ChannelName);
            ws.SetValue(rowNumber, 3, this.Market);
            ws.SetValue(rowNumber, 4, this.Network);
            ws.SetValue(rowNumber, 5, this.Satellite);
            ws.SetValue(rowNumber, 6, this.Tid);
            ws.SetValue(rowNumber, 7, this.Transponder);
            ws.SetValue(rowNumber, 8, this.VPid);
            ws.SetValue(rowNumber, 9, this.SecondaryPid);
            ws.SetValue(rowNumber, 10, this.ShortChannelName);
            ws.SetValue(rowNumber, 11, this.LongChannelName);

            ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;

            if (this._vPid.Trim().Equals(String.Empty))
            {
                if (this._network > 40000)
                {
                    ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.LightGoldenrodYellow);
                }
                else
                {
                    ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.White);
                }
            }
            else if (this._vPid.Equals("1250"))
            {
                ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            }
            else if (this._vPid.Equals("1700"))
            {
                ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            }
            else if (this._vPid.Equals("_") || this._vPid.Equals("-"))
            {
                ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.LightGoldenrodYellow);
            }
            else if (Convert.ToInt32(this._vPid, 16) >= 6144 && Convert.ToInt32(this._vPid, 16) <= 6297)
            {
                ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.LightGoldenrodYellow);
            }
            else if (this._vPid.Equals("0320") || this._vPid.Equals("320"))
            {
                ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.LightGoldenrodYellow);
            }
            else if (this._vPid.Equals("03AC") || this._vPid.Equals("0222") || this._vPid.Equals("222"))
            {
                ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            }
            else if (Convert.ToInt32(this._vPid, 16) >= 4112 && Convert.ToInt32(this._vPid, 16) <= 4240)
            {
                ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            }
            else
            {
                ws.Cells[rowNumber, 1, rowNumber, 11].Style.Fill.BackgroundColor.SetColor(Color.White);
            }
        }

        public override string ToString()
        {
            return this.ChannelNumber + " " + this.ChannelName;
        }
    }
}
