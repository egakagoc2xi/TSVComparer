using System;
using OfficeOpenXml;


namespace TSVComparer.Model
{
    public abstract class ITSVGenericModel
    {
        public int MajorNumber;
        public int MinorNumber;

        protected ITSVGenericModel()
        {
        }

        public abstract string ChannelName { get; }

        public abstract string ChannelNumber { get; }

        public abstract string Market { get; }

        public abstract int Network { get; }

        public abstract int Tid { get; }

        public abstract int Transponder { get; }

        public abstract string VPid { get; }

        public abstract string Satellite { get; }

        public abstract string LongChannelName { get; }

        public abstract string ShortChannelName { get; }

        public abstract void ToExcelRow(ExcelWorksheet ws, int rowNumber);
    }
}
