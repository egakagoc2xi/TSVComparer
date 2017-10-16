using System;
namespace TSVComparer.Model
{
    public class CompareInformation
    {
        private String _OriginalFolder;
        private String _CompareFolder;
        private String _OutputFolder;
        private TypeOfFile _TypeOfFile;
        private TypeOfOutput _TypeOfOutput;
        private Boolean _FullExcelOuput;

        public TypeOfOutput TypeOfOuputFile
        {
            get { return _TypeOfOutput; }
            set { _TypeOfOutput = value; }
        }

        public Boolean FullExcelOuput
        {
            get { return _FullExcelOuput; }
            set { _FullExcelOuput = value; }
        }

        public TypeOfFile TypeOfFileComparisson
        {
            get { return _TypeOfFile; }
            set { _TypeOfFile = value; }
        }

        public String OutputFolder
        {
            get { return _OutputFolder; }
            set { _OutputFolder = value; }
        }


        public String CompareFolder
        {
            get { return _CompareFolder; }
            set { _CompareFolder = value; }
        }

        public String OriginalFolder
        {
            get { return _OriginalFolder; }
            set { _OriginalFolder = value; }
        }

    }
}
