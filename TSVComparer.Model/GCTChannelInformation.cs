﻿using System;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace TSVComparer.Model
{
    public class GCTChannelInformation : ITSVGenericModel
    {
        private string _channelName;
        private string _channelNumber;
        private string _display;
        private string _market;
        private int _network;
        private string _satellite;
        private int _tid;
        private int _transponder;
        private string _vPid;
        private string _longName;

        public GCTChannelInformation(string pData)
        {
            char[] chArray2;
            string[] strArray2;
            char[] separator = new char[] { Convert.ToChar(9) };
            string[] strArray = pData.Trim().Split(separator);
            if (strArray[2].Trim().Contains("-"))
            {
                base.MinorNumber = 0;
                chArray2 = new char[] { Convert.ToChar("-") };
                strArray2 = strArray[2].Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
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
            else if (strArray[2].Trim().Contains("."))
            {
                base.MinorNumber = 0;
                chArray2 = new char[] { Convert.ToChar(".") };
                strArray2 = strArray[2].Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
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
                if (!int.TryParse(strArray[2].Trim(), out this.MajorNumber))
                {
                    base.MajorNumber = -1;
                }
            }
            this._network = string.IsNullOrWhiteSpace(strArray[0].Trim()) ? 0 : Convert.ToInt32(strArray[0].Trim());
            this._channelName = strArray[1].Trim();
            this._channelNumber = strArray[2].Trim();
            this._transponder = (string.IsNullOrWhiteSpace(strArray[3].Trim()) || strArray[3].Trim().Equals("_")) ? 0 : Convert.ToInt32(strArray[3].Trim());
            this._satellite = strArray[4].Trim();
            this._display = strArray[5].Trim();
            if (strArray.Length >= 7)
            {
                this._tid = string.IsNullOrWhiteSpace(strArray[6].Trim()) ? -1 : Convert.ToInt32(strArray[6].Trim());
            }
            else
            {
                this._tid = -1;
            }

            if (strArray.Length >= 8)
            {
                this._vPid = string.IsNullOrWhiteSpace(strArray[7].Trim()) ? String.Empty : strArray[7].Trim();
            }
            else
            {
                this._vPid = string.Empty;
            }

            if (strArray.Length >= 9)
            {
                this._market = string.IsNullOrWhiteSpace(strArray[8].Trim()) ? String.Empty : strArray[8].Trim();
            }
            else
            {
                this._market = string.Empty;
            }

            if (strArray.Length >= 11)
            {
                this._longName = string.IsNullOrWhiteSpace(strArray[10].Trim()) ? String.Empty : strArray[10].Trim();
            }
            else
            {
                this._longName = string.Empty;
            }


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

        public string Display
        {
            get
            {
                return this._display;
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
                return this._network;
            }
        }

        public override string Satellite
        {
            get
            {
                return this._satellite;
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
                else if (this._vPid.Equals("_") || this._vPid.Equals("-"))
                {
                    return "OTA Record";
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

        public override string LongChannelName
        {
            get
            {
                return _longName;
            }
        }

        public override string ShortChannelName
        {
            get
            {
                return string.Empty;
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
            ws.SetValue(rowNumber, 10, this._longName);

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
