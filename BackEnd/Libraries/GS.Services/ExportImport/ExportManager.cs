using GS.Core;
using GS.Core.Domain.BaoCaos;
using GS.Services.DanhMuc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace GS.Services.ExportImport
{
    public partial class ExportManager : IExportManager
    {
        #region Fields
        #endregion

        #region Ctor
        public ExportManager()
        {
        }
        #endregion

        #region Methods
        public void ExportXlsxReportSoTL_AHS_PT(Stream stream, LayoutReport layoutReport, string workSheetName)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add(workSheetName);
                worksheet.DefaultRowHeight = 20;
                worksheet.Cells["A1:E1"].Merge = true;
                worksheet.Cells["A1"].Value = layoutReport.ThongTinHeader != null ? layoutReport.ThongTinHeader.HeaderLeft : "";
                worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells["A1"].Style.Font.Size = 12;
                worksheet.Cells["A1"].Style.Font.Name = "Times New Roman";
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["I2:P4"].Merge = true;
                worksheet.Cells["I2"].Value = layoutReport.ThongTinHeader != null ? layoutReport.ThongTinHeader.HeaderCenter : "";
                worksheet.Cells["I2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["I2"].Style.Font.Size = 12;
                worksheet.Cells["I2"].Style.Font.Name = "Times New Roman";
                worksheet.Cells["I2"].Style.Font.Bold = true;
                worksheet.Cells["S1:Z3"].Merge = true;
                worksheet.Cells["S1"].Value = layoutReport.ThongTinHeader != null ? layoutReport.ThongTinHeader.HeaderRight : "";
                worksheet.Cells["S1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["S1"].Style.Font.Size = 12;
                worksheet.Cells["S1"].Style.Font.Name = "Times New Roman";
                worksheet.Cells["S1"].Style.Font.Bold = true;
                foreach (var n in layoutReport.ListColumeHeader)
                {
                    PrepareWidthColume(n);
                }
                int colStart = 6;
                int rowStart = layoutReport.hasRowIndex ? 2 : 1;
                int rowIndex = 1;
                var ListHeightCol = new List<ThongTinHeightColume>();
                string lastRowRequest = "firstValue";
                foreach (var header in layoutReport.ListColumeHeader)
                {
                    PrepareWorkSheet(header, ListHeightCol, ref worksheet, ref colStart, ref rowStart, ref rowIndex, ref lastRowRequest);
                }
                var maxRow = ListHeightCol.Select(c => c.RowIndex).Max();
                var modelTable = worksheet.Cells[6, 1, maxRow, rowStart - 1];
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                modelTable.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                modelTable.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                modelTable.Style.WrapText = true;
                modelTable.Style.Font.Size = 12;
                modelTable.Style.Font.Bold = true;
                modelTable.Style.Font.Name = "Times New Roman";
                // row index
                if (layoutReport.hasRowIndex)
                {
                    worksheet.Cells[6, 1, maxRow, 1].Merge = true;
                    worksheet.Cells[6, 1].Value = "STT";
                }
                if (layoutReport.ListvalueContent != null && layoutReport.ListvalueContent.Count > 0)
                {
                    // column index
                    if (layoutReport.hasColumnIndex)
                    {
                        int IndexDataRow = layoutReport.hasRowIndex ? 2 : 1;
                        int ind = 1;
                        foreach (var ci in layoutReport.ListvalueContent.FirstOrDefault().ValueContent)
                        {
                            worksheet.Cells[maxRow + 1, IndexDataRow].Value = ind;
                            IndexDataRow = IndexDataRow + 1;
                            ind++;
                        }
                    }
                    // data content
                    int valRowStart = 1;
                    if (layoutReport.hasColumnIndex)
                        valRowStart = maxRow + 2;
                    else
                        valRowStart = maxRow + 1;
                    int valColStart = layoutReport.hasRowIndex ? 2 : 1;
                    int stt = 1;
                    foreach (var content in layoutReport.ListvalueContent)
                    {
                        valColStart = layoutReport.hasRowIndex ? 2 : 1;
                        foreach (var ct in content.ValueContent)
                        {
                            worksheet.Cells[valRowStart, valColStart].Value = ct != null ? ct : "";
                            valColStart = valColStart + 1;
                        }
                        if (layoutReport.hasRowIndex)
                            worksheet.Cells[valRowStart, 1].Value = stt;
                        stt++;
                        valRowStart = valRowStart + 1;
                    }
                    var modeltableValue = worksheet.Cells[maxRow, 1, valRowStart - 1, valColStart - 1];
                    modeltableValue.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    modeltableValue.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    modeltableValue.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    modeltableValue.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    modeltableValue.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    modeltableValue.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    modeltableValue.Style.WrapText = true;
                    modeltableValue.Style.Font.Size = 12;
                    modeltableValue.Style.Font.Name = "Times New Roman";
                    //last row
                    int startRow = layoutReport.hasColumnIndex ? (maxRow + 2) : (maxRow + 1);
                    int endRow = valRowStart - 1;
                    var listRequest = lastRowRequest.Split(",");
                    foreach (var request in listRequest)
                    {
                        if (request != "firstValue")
                        {
                            double content = 0;
                            var contentRequest = request.Split("-");
                            int colInd = Convert.ToInt32(contentRequest[0]);
                            if (contentRequest[1] == "1")
                            {
                                for (int i = startRow; i <= endRow; i++)
                                {
                                    content = content + worksheet.Cells[i, colInd].GetValue<int>();
                                }
                            }
                            if (contentRequest[1] == "2")
                            {
                                for (int i = startRow; i <= endRow; i++)
                                {
                                    content = content + worksheet.Cells[i, colInd].GetValue<int>();
                                }
                                content = content / (endRow - startRow + 1);
                            }
                            if (contentRequest[1] == "3")
                            {
                                for (int i = startRow; i <= endRow; i++)
                                {
                                    if (worksheet.Cells[i, colInd].GetValue<int>() < content || content == 0)
                                    {
                                        content = worksheet.Cells[i, colInd].GetValue<int>();
                                    }
                                }
                            }
                            if (contentRequest[1] == "4")
                            {
                                for (int i = startRow; i <= endRow; i++)
                                {
                                    if (worksheet.Cells[i, colInd].GetValue<int>() > content || content == 0)
                                    {
                                        content = worksheet.Cells[i, colInd].GetValue<int>();
                                    }
                                }
                            }
                            worksheet.Cells[(endRow + 1), Convert.ToInt32(contentRequest[0])].Value = content;
                        }
                    }
                    var lastRow = worksheet.Cells[(endRow + 1), 1, (endRow + 1), (valColStart - 1)];
                    lastRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    lastRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    lastRow.Style.WrapText = true;
                    lastRow.Style.Font.Size = 13;
                    lastRow.Style.Font.Name = "Times New Roman";
                    lastRow.Style.Font.Bold = true;
                }
                worksheet.PrinterSettings.PaperSize = ePaperSize.A3;

                xlPackage.Save();
            }
        }
        public void ExportXlsxEntity(Stream stream, LayoutReport layoutReport, string workSheetName, bool addSTT)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            using (var xlPackage = new ExcelPackage(stream))
            {
                int total = layoutReport.ListvalueContent.Count();
                int pageSize = 10000;
                int TotalPages = total / pageSize;

                if (total % pageSize > 0)
                    TotalPages++;
                for (int x = 0; x < TotalPages; x++)
                {
                    var lst = layoutReport.ListvalueContent.Skip(x * pageSize).Take(pageSize).ToList();
                    LayoutReport layoutReportSplit = new LayoutReport();
                    if (addSTT == true)
                    {
                        layoutReportSplit = new LayoutReport()
                        {
                            hasColumnIndex = layoutReport.hasColumnIndex,
                            hasRowIndex = layoutReport.hasRowIndex,
                            ListvalueContent = lst,
                            ListColumeHeader = layoutReport.ListColumeHeader
                        };
                    }
                    else
                    {
                        layoutReportSplit = new LayoutReport()
                        {
                            hasColumnIndex = layoutReport.hasColumnIndex,
                            hasRowIndex = false,
                            ListvalueContent = lst,
                            ListColumeHeader = layoutReport.ListColumeHeader
                        };
                    }
                    ExcelWorksheet worksheet = null;
                    worksheet = xlPackage.Workbook.Worksheets.Add(workSheetName + "_" + x);
                    worksheet.DefaultRowHeight = 20;
                    //if (layoutReportSplit.hasRowIndex)
                    //{
                    //    InputHeaderReportModel inp = new InputHeaderReportModel() { };
                    //}    
                    foreach (var n in layoutReportSplit.ListColumeHeader)
                    {
                        PrepareWidthColume(n);
                    }
                    int colStart = 1;
                    int rowStart = layoutReportSplit.hasRowIndex ? 2 : 1;
                    int rowIndex = 1;
                    var ListHeightCol = new List<ThongTinHeightColume>();
                    string lastRowRequest = "firstValue";
                    foreach (var header in layoutReportSplit.ListColumeHeader)
                    {
                        PrepareWorkSheet(header, ListHeightCol, ref worksheet, ref colStart, ref rowStart, ref rowIndex, ref lastRowRequest);
                    }
                    var maxRow = ListHeightCol.Select(c => c.RowIndex).Max();
                    var modelTable = worksheet.Cells[1, 1, maxRow, rowStart - 1];
                    modelTable.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    modelTable.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    modelTable.Style.Font.Size = 11;
                    //modelTable.Style.Font.Bold = true;
                    modelTable.Style.Font.Name = "Calibri";
                    worksheet.View.FreezePanes(2, 1);

                    if (layoutReportSplit.ListvalueContent != null && layoutReportSplit.ListvalueContent.Count > 0)
                    {
                        // data content
                        int valRowStart = 1;
                        if (layoutReportSplit.hasColumnIndex)
                            valRowStart = maxRow + 2;
                        else
                            valRowStart = maxRow + 1;
                        int valColStart = layoutReportSplit.hasRowIndex ? 2 : 1;
                        int stt = 1;
                        if (addSTT == true)
                        {
                            if (layoutReportSplit.hasRowIndex)
                                worksheet.Cells[1, 1].Value = "STT";
                            foreach (var content in layoutReportSplit.ListvalueContent)
                            {
                                valColStart = layoutReportSplit.hasRowIndex ? 2 : 1;
                                foreach (var ct in content.ValueContent)
                                {
                                    worksheet.Cells[valRowStart, valColStart].Value = ct != null ? ct : "";
                                    valColStart = valColStart + 1;
                                }
                                if (layoutReportSplit.hasRowIndex)
                                    worksheet.Cells[valRowStart, 1].Value = stt;
                                stt++;
                                valRowStart = valRowStart + 1;
                            }
                        }

                        else
                        {
                            foreach (var content in layoutReportSplit.ListvalueContent)
                            {
                                valColStart = layoutReportSplit.hasRowIndex ? 2 : 1;
                                foreach (var ct in content.ValueContent)
                                {
                                    worksheet.Cells[valRowStart, valColStart].Value = ct != null ? ct : "";
                                    valColStart = valColStart + 1;
                                }
                                if (layoutReportSplit.hasRowIndex)
                                    worksheet.Cells[valRowStart, 1].Value = stt;
                                stt++;
                                valRowStart = valRowStart + 1;
                            }
                        }
                        
                        var modeltableValue = worksheet.Cells[maxRow, 1, valRowStart - 1, valColStart - 1];
                        modeltableValue.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        modeltableValue.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        modeltableValue.Style.WrapText = false;
                        modeltableValue.Style.Font.Size = 11;
                        modeltableValue.Style.Font.Name = "Calibri";
                        //last row
                        int startRow = layoutReportSplit.hasColumnIndex ? (maxRow + 2) : (maxRow + 1);
                        int endRow = valRowStart - 1;
                        var listRequest = lastRowRequest.Split(",");
                        foreach (var request in listRequest)
                        {
                            if (request != "firstValue")
                            {
                                double content = 0;
                                var contentRequest = request.Split("-");
                                int colInd = Convert.ToInt32(contentRequest[0]);
                                if (contentRequest[1] == "1")
                                {
                                    for (int i = startRow; i <= endRow; i++)
                                    {
                                        content = content + worksheet.Cells[i, colInd].GetValue<int>();
                                    }
                                }
                                if (contentRequest[1] == "2")
                                {
                                    for (int i = startRow; i <= endRow; i++)
                                    {
                                        content = content + worksheet.Cells[i, colInd].GetValue<int>();
                                    }
                                    content = content / (endRow - startRow + 1);
                                }
                                if (contentRequest[1] == "3")
                                {
                                    for (int i = startRow; i <= endRow; i++)
                                    {
                                        if (worksheet.Cells[i, colInd].GetValue<int>() < content || content == 0)
                                        {
                                            content = worksheet.Cells[i, colInd].GetValue<int>();
                                        }
                                    }
                                }
                                if (contentRequest[1] == "4")
                                {
                                    for (int i = startRow; i <= endRow; i++)
                                    {
                                        if (worksheet.Cells[i, colInd].GetValue<int>() > content || content == 0)
                                        {
                                            content = worksheet.Cells[i, colInd].GetValue<int>();
                                        }
                                    }
                                }
                                worksheet.Cells[(endRow + 1), Convert.ToInt32(contentRequest[0])].Value = content;
                            }
                        }
                        var lastRow = worksheet.Cells[(endRow + 1), 1, (endRow + 1), (valColStart - 1)];
                        lastRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        lastRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        lastRow.Style.WrapText = false;
                        lastRow.Style.Font.Size = 11;
                        lastRow.Style.Font.Name = "Calibri";
                        //lastRow.Style.Font.Bold = true;
                    }
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A3;

                }

                xlPackage.Save();
            }
        }
        public void PrepareWorkSheet(ThongTinBaoCaoColume col, List<ThongTinHeightColume> list, ref ExcelWorksheet excelWorksheet, ref int colStart, ref int rowStart, ref int rowIndex, ref string lastRowRequest)
        {
            int colEnd = colStart + col.ColumeHeaderRowspan - 1;
            int rowEnd = rowStart + col.ColumeHeaderColspan - 1;
            excelWorksheet.Cells[colStart, rowStart, colEnd, rowEnd].Merge = true;
            excelWorksheet.Cells[colStart, rowStart].Value = col.ColumeHeaderName != null ? col.ColumeHeaderName : "";
            if (col.ListSubLayout != null && col.ListSubLayout.Count > 0)
            {
                var colNext = colStart + col.ColumeHeaderRowspan;
                foreach (var a in col.ListSubLayout)
                {
                    PrepareWorkSheet(a, list, ref excelWorksheet, ref colNext, ref rowStart, ref rowIndex, ref lastRowRequest);
                }
            }
            else
            {
                if (col.TypeLastRowId != 0)
                {
                    lastRowRequest = lastRowRequest + "," + rowEnd + "-" + col.TypeLastRowId;
                }
                excelWorksheet.Column(rowStart).Width = col.ColumeHeaderWidthValue;
                rowStart = rowEnd + 1;
                rowIndex++;
            }

            var height = MeasureTextHeight(col.ColumeHeaderName, col.ColumeHeaderWidthValue);
            if (height < 19.5)
            {
                height = 19.5;
            }
            var heightCol = new ThongTinHeightColume()
            {
                RowHeight = height,
                RowIndex = colEnd
            };
            list.Add(heightCol);
            var heightMax = list.Where(c => c.RowIndex == colEnd).Select(c => c.RowHeight).Max();
            if (heightMax <= height)
            {
                excelWorksheet.Row(colEnd).Height = height;
            }
        }
        public void PrepareWidthColume(ThongTinBaoCaoColume col)
        {
            if (col.ListSubLayout != null && col.ListSubLayout.Count > 0)
            {
                foreach (var n in col.ListSubLayout)
                {
                    PrepareWidthColume(n);
                    col.ColumeHeaderWidthValue = col.ColumeHeaderWidthValue + n.ColumeHeaderWidthValue;
                }
            }
            else
            {
                int count = col.ColumeHeaderName.Length;
                col.ColumeHeaderWidthValue = count * 2;
                //if (count <= 20)
                //{
                //    col.ColumeHeaderWidthValue = 7.14;
                //}
                //else if (count <= 50)
                //{
                //    col.ColumeHeaderWidthValue = 9;
                //}
                //else if (count <= 70)
                //{
                //    col.ColumeHeaderWidthValue = 12;
                //}
                //else if (count <= 100)
                //{
                //    col.ColumeHeaderWidthValue = 16;
                //}
                //else if (count <= 150)
                //{
                //    col.ColumeHeaderWidthValue = 18.5;
                //}
                //else
                //{
                //    col.ColumeHeaderWidthValue = 21;
                //}
            }
        }
        public double MeasureTextHeight(string text, Double width)
        {
            if (string.IsNullOrEmpty(text)) return 0.0;
            var bitmap = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(bitmap);

            var pixelWidth = Convert.ToInt32(width * 7.5);  //7.5 pixels per excel column width
            var drawingFont = new Font("Times New Roman", 12);
            var size = graphics.MeasureString(text, drawingFont, pixelWidth);

            //72 DPI and 96 points per inch.  Excel height in points with max of 409 per Excel requirements.
            return Math.Min(Convert.ToDouble(size.Height) * 72 / 96, 409);
        }
        #endregion
    }
}
