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
        protected virtual void ExcelSaveFileCommandExecute(String pPath, Boolean pOutputFullExcelFile)
        {
            if (!pPath.Substring(pPath.Length - 1, 1).Equals("/")) pPath += "/";
            String path = pPath + DateTime.Now.ToString("yyMMddHHmmss") + "_TsvComparer.xlsx";
            FileInfo fileInfo = new FileInfo(path);

            ExcelPackage package = new ExcelPackage(fileInfo);

            int worksheets = 1;

            if (pOutputFullExcelFile)
            {
                package.Workbook.Worksheets.Add("Original");
                package.Workbook.Worksheets.Add("Compare");

            }
            package.Workbook.Worksheets.Add("Analysis");

            if (pOutputFullExcelFile)
            {
                ExcelWorksheet wsOriginal = package.Workbook.Worksheets[worksheets];
                wsOriginal.Name = "Original"; //Setting Sheet's name
                wsOriginal.Cells.Style.Font.Size = 10; //Default font size for whole sheet
                wsOriginal.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet
                ExcelChannelListTab(wsOriginal, this.OriginalChannelListViewModelProperty.ChannelInformation);

                worksheets++;

                ExcelWorksheet wsCompare = package.Workbook.Worksheets[worksheets];
                wsCompare.Name = "Compare"; //Setting Sheet's name
                wsCompare.Cells.Style.Font.Size = 10; //Default font size for whole sheet
                wsCompare.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet
                ExcelChannelListTab(wsCompare, this.CompareChannelListViewModelProperty.ChannelInformation);

                worksheets++;
            }

            ExcelWorksheet wsAnalysis = package.Workbook.Worksheets[worksheets];
            wsAnalysis.Name = "Analysis"; //Setting Sheet's name
            wsAnalysis.Cells.Style.Font.Size = 10; //Default font size for whole sheet
            wsAnalysis.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet
            ExcelAnalysisTab(wsAnalysis);

            package.Save();
        }

        protected virtual void HtmlSaveFileCommandExecute(String pPath)
        {
            if (!pPath.Substring(pPath.Length - 1, 1).Equals("/")) pPath += "/";
            String path = pPath + DateTime.Now.ToString("yyMMddHHmmss") + "_TsvComparer.html";

            StreamWriter writer = new StreamWriter(File.Create(path));
            writer.WriteLine("<html><head></head><body>");

            writer.WriteLine("<span><b>Agregados</b></span>");
            writer.WriteLine("<br/>");
            writer.WriteLine("<table>");
            writer.WriteLine("<tr><td>Numero</td><td>Nombre</td><td>Mercado</td><td>Red</td><td>TID</td><td>TP</td><td>VPID</td></tr>");
            foreach (CompareChannelInformation information in this.ResultViewModelProperty.AddedChannelInformation) writer.WriteLine(information.ToHtmlString());
            writer.WriteLine("</table>");
            writer.WriteLine("<br/>");

            writer.WriteLine("<span><b>Eliminados</b></span>");
            writer.WriteLine("<br/>");
            writer.WriteLine("<table>");
            writer.WriteLine("<tr><td>Numero</td><td>Nombre</td><td>Mercado</td><td>Red</td><td>TID</td><td>TP</td><td>VPID</td></tr>");
            foreach (CompareChannelInformation information in this.ResultViewModelProperty.RemovedChannelInformation) writer.WriteLine(information.ToHtmlString());
            writer.WriteLine("</table>");
            writer.WriteLine("<br/>");

            writer.WriteLine("<span><b>Renombrados</b></span>");
            writer.WriteLine("<br/>");
            writer.WriteLine("<table>");
            writer.WriteLine("<tr><td>Numero</td><td>Nombre</td><td>Nombre Anterior</td><td>Mercado</td><td>Red</td><td>TID</td><td>TP</td><td>VPID</td></tr>");
            foreach (CompareChannelInformation information in this.ResultViewModelProperty.RenamedChannelInformation) writer.WriteLine(information.ToHtmlString());
            writer.WriteLine("</table>");
            writer.WriteLine("<br/>");


            writer.WriteLine("<span><b>Reubicados</b></span>");
            writer.WriteLine("<br/>");
            writer.WriteLine("<table>");
            writer.WriteLine("<tr><td>Numero</td><td>Nombre</td><td>Mercado</td><td>Red</td><td>TID</td><td>TP</td><td>VPID</td><td><---------</td><td>Red O.</td><td>TID O.</td><td>TP O.</td><td>VPID O.</td></tr>");
            foreach (CompareChannelInformation information in this.ResultViewModelProperty.RemapedChannelInformation) writer.WriteLine(information.ToHtmlString());
            writer.WriteLine("</table>");
            writer.WriteLine("<br/>");

            writer.WriteLine("</body></html>");

            writer.Dispose();
        }

        protected virtual void BBCodeSaveFileCommandExecute(String pPath)
        {
            try
            {
                if (!pPath.Substring(pPath.Length - 1, 1).Equals("/")) pPath += "/";
                String path = pPath + DateTime.Now.ToString("yyMMddHHmmss") + "_TsvComparerBBCode.bbc";

                //this.SetLog("Dumping File: " + path);

                StreamWriter writer = new StreamWriter(File.Create(path));

                writer.WriteLine("[color=#000000][size=4][font=georgia,serif][b]Agregados[/b][/font][/size][/color]");
                writer.WriteLine("[size=2][b][color=#000000]Numero[/color][color=#ffffff]ooooooooo[/color][color=#000000]Nombre[/color][color=#ffffff]oooooooo[/color][color=#000000]Mercado[/color][color=#ffffff]ooooooooo[/color][color=#000000]Red[/color][color=#ffffff]oooooooooooo[/color][color=#000000]TID[/color][color=#ffffff]oooooooooooo[/color][color=#000000]TP[/color][color=#ffffff]ooooooooooooo[/color][color=#000000]VPID[/color][color=#ffffff]ooooooooooo[/color][/b][/size]");
                foreach (CompareChannelInformation information in this.ResultViewModelProperty.AddedChannelInformation) writer.WriteLine(information.ToBBCodeString());
                writer.WriteLine("[size=1][color=#ffffff]oooooo[/color][/size]");
                writer.WriteLine("[size=1][color=#ffffff]oooooo[/color][/size]");

                writer.WriteLine("[color=#000000][size=4][font=georgia,serif][b]Eliminados[/b][/font][/size][/color]");
                writer.WriteLine("[size=2][b][color=#000000]Numero[/color][color=#ffffff]ooooooooo[/color][color=#000000]Nombre[/color][color=#ffffff]oooooooo[/color][color=#000000]Mercado[/color][color=#ffffff]ooooooooo[/color][color=#000000]Red[/color][color=#ffffff]oooooooooooo[/color][color=#000000]TID[/color][color=#ffffff]oooooooooooo[/color][color=#000000]TP[/color][color=#ffffff]ooooooooooooo[/color][color=#000000]VPID[/color][color=#ffffff]ooooooooooo[/color][/b][/size]");
                foreach (CompareChannelInformation information in this.ResultViewModelProperty.RemovedChannelInformation) writer.WriteLine(information.ToBBCodeString());
                writer.WriteLine("[size=1][color=#ffffff]oooooo[/color][/size]");
                writer.WriteLine("[size=1][color=#ffffff]oooooo[/color][/size]");

                writer.WriteLine("[color=#000000][size=4][font=georgia,serif][b]Renombrados[/b][/font][/size][/color]");
                writer.WriteLine("[size=2][b][color=#000000]Numero[/color][color=#ffffff]ooooooooo[/color][color=#000000]Nombre[/color][color=#ffffff]oooooooo[/color][color=#000000]Anterior[/color][color=#ffffff]oooooo[/color][color=#000000]Mercado[/color][color=#ffffff]ooooooooo[/color][color=#000000]Red[/color][color=#ffffff]oooooooooooo[/color][color=#000000]TID[/color][color=#ffffff]oooooooooooo[/color][color=#000000]TP[/color][color=#ffffff]ooooooooooooo[/color][color=#000000]VPID[/color][color=#ffffff]ooooooooooo[/color][/b][/size]");
                foreach (CompareChannelInformation information in this.ResultViewModelProperty.RenamedChannelInformation) writer.WriteLine(information.ToBBCodeString());
                writer.WriteLine("[size=1][color=#ffffff]oooooo[/color][/size]");
                writer.WriteLine("[size=1][color=#ffffff]oooooo[/color][/size]");

                writer.WriteLine("[color=#000000][size=4][font=georgia,serif][b]Reubicados[/b][/font][/size][/color]");
                writer.WriteLine("[size=2][b][color=#000000]Numero[/color][color=#ffffff]ooooooooo[/color][color=#000000]Nombre[/color][color=#ffffff]oooooooo[/color][color=#000000]Mercado[/color]WW[color=#ffffff]ooooooooo[/color][color=#000000]Red[/color][color=#ffffff]oooooooooooo[/color][color=#000000]TID[/color][color=#ffffff]oooooooooooo[/color][color=#000000]TP[/color][color=#ffffff]ooooooooooooo[/color][color=#000000]VPID[/color][color=#ffffff]ooooooooooo[/color][color=#000000]<--------- Red Original[/color][color=#ffffff]ooo[/color][color=#000000]TID Original[/color][color=#ffffff]ooo[/color][color=#000000]TP Original[/color][color=#ffffff]oooo[/color][color=#000000]VPID Original[/color][color=#ffffff]oo[/color][/b][/size]");
                foreach (CompareChannelInformation information in this.ResultViewModelProperty.RemapedChannelInformation) writer.WriteLine(information.ToBBCodeString());

                writer.Dispose();
            }

            //this.SetLog("Dumped File: " + path);
            catch
            {
                //this.SetLog("Error dumping the file: " + exception.Message);
            }
        }

        private void ExcelChannelListTab(ExcelWorksheet ws, ObservableCollection<ITSVGenericModel> list)
        {
            int rows = list.Count + 1;

            ws.InsertRow(1, rows);
            ws.InsertColumn(1, 11);

            int rowNumber = 1;

            ws.SetValue(rowNumber, 1, "Channel");
            ws.SetValue(rowNumber, 2, "Name");
            ws.SetValue(rowNumber, 3, "Market");
            ws.SetValue(rowNumber, 4, "NET");
            ws.SetValue(rowNumber, 5, "SAT");
            ws.SetValue(rowNumber, 6, "TID");
            ws.SetValue(rowNumber, 7, "TPN");
            ws.SetValue(rowNumber, 8, "VPID");
            ws.SetValue(rowNumber, 9, "Sec VPID");
            ws.SetValue(rowNumber, 10, "Notes");
            ws.SetValue(rowNumber, 11, "Notes 2");

            ws.Cells[rowNumber, 1, rowNumber, 11].Style.Font.Bold = true;

            foreach (ITSVGenericModel information in list) information.ToExcelRow(ws, ++rowNumber);

            var cellBorder = ws.Cells[1, 1, rows, 11].Style.Border;
            cellBorder.Bottom.Style = cellBorder.Top.Style = cellBorder.Left.Style = cellBorder.Right.Style = ExcelBorderStyle.Thin;

        }

        private void ExcelAnalysisTab(ExcelWorksheet ws)
        {
            int rows = this.ResultViewModelProperty.AddedChannelInformation.Count;
            rows += this.ResultViewModelProperty.RemovedChannelInformation.Count;
            rows += this.ResultViewModelProperty.RenamedChannelInformation.Count;
            rows += this.ResultViewModelProperty.RemapedChannelInformation.Count;
            rows += 8;

            ws.InsertRow(1, rows);
            ws.InsertColumn(1, 14);

            int rowNumber = 1;

            ws.SetValue(rowNumber, 1, "Channel");
            ws.SetValue(rowNumber, 2, "Name");
            ws.SetValue(rowNumber, 3, "Market");
            ws.SetValue(rowNumber, 4, "Current NET");
            ws.SetValue(rowNumber, 5, "Current SAT");
            ws.SetValue(rowNumber, 6, "Current TID");
            ws.SetValue(rowNumber, 7, "Current TPN");
            ws.SetValue(rowNumber, 8, "Current VPID");
            ws.SetValue(rowNumber, 9, "Previous Name");
            ws.SetValue(rowNumber, 10, "Previous NET");
            ws.SetValue(rowNumber, 11, "Previous SAT");
            ws.SetValue(rowNumber, 12, "Previous TID");
            ws.SetValue(rowNumber, 13, "Previous TPN");
            ws.SetValue(rowNumber, 14, "Previous VPID");

            ws.Cells[rowNumber, 4, rowNumber, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[rowNumber, 4, rowNumber, 8].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

            ws.Cells[rowNumber, 9, rowNumber, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[rowNumber, 9, rowNumber, 14].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

            ws.Cells[rowNumber, 1, rowNumber, 14].Style.Font.Bold = true;

            ws.SetValue(++rowNumber, 1, "Added");
            ws.Cells[rowNumber, 1, rowNumber, 14].Style.Font.Bold = true;
            ws.Cells[rowNumber, 1, rowNumber, 14].Merge = true;

            foreach (CompareChannelInformation information in this.ResultViewModelProperty.AddedChannelInformation) information.ToExcelRow(ws, ++rowNumber);

            ++rowNumber;
            ws.SetValue(++rowNumber, 1, "Removed");
            ws.Cells[rowNumber, 1, rowNumber, 14].Style.Font.Bold = true;
            ws.Cells[rowNumber, 1, rowNumber, 14].Merge = true;

            foreach (CompareChannelInformation information in this.ResultViewModelProperty.RemovedChannelInformation) information.ToExcelRow(ws, ++rowNumber);

            ++rowNumber;
            ws.SetValue(++rowNumber, 1, "Renamed");
            ws.Cells[rowNumber, 1, rowNumber, 14].Style.Font.Bold = true;
            ws.Cells[rowNumber, 1, rowNumber, 14].Merge = true;

            foreach (CompareChannelInformation information in this.ResultViewModelProperty.RenamedChannelInformation) information.ToExcelRow(ws, ++rowNumber);

            ++rowNumber;
            ws.SetValue(++rowNumber, 1, "Remapped");
            ws.Cells[rowNumber, 1, rowNumber, 14].Style.Font.Bold = true;
            ws.Cells[rowNumber, 1, rowNumber, 14].Merge = true;

            foreach (CompareChannelInformation information in this.ResultViewModelProperty.RemapedChannelInformation) information.ToExcelRow(ws, ++rowNumber);

            var cellBorder = ws.Cells[1, 1, rows, 14].Style.Border;
            cellBorder.Bottom.Style = cellBorder.Top.Style = cellBorder.Left.Style = cellBorder.Right.Style = ExcelBorderStyle.Thin;

            ws.Cells[1, 1, rows, 11].Style.WrapText = true;
            ws.Cells[1, 1, rows, 11].Style.ShrinkToFit = true;
        }

        public virtual void TSVSaveFileCommandExecute(String pPath)
        {
            try
            {
                if (!pPath.Substring(pPath.Length - 1, 1).Equals("/")) pPath += "/";

                String path = pPath + DateTime.Now.ToString("yyMMddHHmmss") + "_TsvComparer.tsv";
                //this.SetLog("Dumping File: " + path);


                StreamWriter writer = new StreamWriter(File.Create(path));
                writer.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}", new object[] { "Channel Number", "Channel Name", "Previous Channel Name", "Mercado", "Network Destination", "TID Destination", "Transponder Destination", "PID Destination", "Type", "Network Original", "TID Orginal", "Transponder Original", "PID Original" }));
                foreach (CompareChannelInformation information in this.ResultViewModelProperty.AddedChannelInformation) writer.WriteLine(information.ToString());
                foreach (CompareChannelInformation information in this.ResultViewModelProperty.RemovedChannelInformation) writer.WriteLine(information.ToString());
                foreach (CompareChannelInformation information in this.ResultViewModelProperty.RenamedChannelInformation) writer.WriteLine(information.ToString());
                foreach (CompareChannelInformation information in this.ResultViewModelProperty.RemapedChannelInformation) writer.WriteLine(information.ToString());
                writer.Dispose();
                //this.SetLog("Dumped File: " + path);
            }
            catch
            {
                //this.SetLog("Error dumping the file: " + exception.Message);
            }
        }
    }
}
