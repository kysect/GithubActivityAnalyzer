using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using Kysect.GithubActivityAnalyzer.DetailedStats;

namespace Kysect.GithubActivityAnalyzer.ExcelExport
{
    public class ExcelExportManager
    {
        public List<GroupInfo> Info { get; set; }
        private IXLWorkbook Workbook { get; }

        public ExcelExportManager(List<GroupInfo> info)
        {
            Info = info;
            Workbook = new XLWorkbook();
        }

        public void SaveExcel(string path)
        {
            Workbook.SaveAs(path);
        }

        public IXLWorkbook ExportShortInfo()
        {
            foreach (var groupInfo in Info)
            {
                IXLWorksheet worksheet = Workbook.Worksheets.Add($"{groupInfo.Group.GroupName}");
                worksheet.Cell(2, 1).Value = "Среднее количество коммитов за месяц:";
                worksheet.Cell(3, 1).Value = "Всего коммитов за месяц:";
                worksheet.Cell(4, 1).Value = "Котик месяца:";
                worksheet.Cell(5, 1).Value = "Кандидат на ремень по жопе:";
                worksheet.Row(1).SetDataType(XLDataType.Text);
                for(int column = 2; column<groupInfo.Statistics.Count; column++)
                {
                    worksheet.Cell(1, column).Value = groupInfo.Statistics[column-2].Month;
                    worksheet.Cell(1, column).Style.DateFormat.Format = "MMMM-yyyy";
                    worksheet.Cell(2, column).Value = groupInfo.Statistics[column - 2].AverageValue;
                    worksheet.Cell(3, column).Value = groupInfo.Statistics[column - 2].TotalContributions;
                    worksheet.Cell(4, column).Value =
                        groupInfo.Statistics[column-2].MaxValueStudent.Item1.Username + ":" + groupInfo.Statistics[column - 2].MaxValueStudent.Item2;
                    worksheet.Cell(5, column).Value =
                        groupInfo.Statistics[column - 2].MinValueStudent.Item1.Username + ":" + groupInfo.Statistics[column - 2].MinValueStudent.Item2;
                }
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
            }
            return Workbook;
        }

        public IXLWorkbook ExportDetailedInfo()
        {
            foreach (var info in Info)
            {
                IXLWorksheet worksheetDetailed = Workbook.Worksheets.Add($"{info.Group.GroupName}-DetailedStat");

                for (int row = 2; row < info.Group.Students.Count + 2; row++)
                {
                    worksheetDetailed.Cell(row, 1).Value = info.Group.Students[row - 2].Username;
                }
                int column = 2;

                int lastColumn = 0;
                int lastRow = 0;

                foreach (var stat in info.Statistics)
                {
                    worksheetDetailed.Cell(1, column).Value = stat.Month;
                    worksheetDetailed.Cell(1, column).Style.DateFormat.Format = "MMMM-yyyy";
                    int row = 2; ;
                    foreach (var studentResult in stat.DetailedStat)
                    {
                        worksheetDetailed.Cell(row, column).Value = studentResult.Item2;
                        lastRow = row;
                        row++;
                    }

                    IXLAddress address1 = worksheetDetailed.Cell(2, column).Address;
                    IXLAddress address2 = worksheetDetailed.Cell(lastRow, column).Address;

                    var cellWithFormulaMonthSum = worksheetDetailed.Cell(row, column);
                    cellWithFormulaMonthSum.FormulaA1 = $"=SUM({address1}:{address2})";
                    column++;
                    lastColumn = column;
                }


                for (int j = 2; j <= lastRow; j++)
                {
                    IXLAddress address1 = worksheetDetailed.Cell(j, 2).Address;
                    IXLAddress address2 = worksheetDetailed.Cell(j, lastColumn-1).Address;
                    var cellWithFormula = worksheetDetailed.Cell(j, lastColumn);
                    cellWithFormula.FormulaA1 = $"=SUM({address1}:{address2})";
                }
                worksheetDetailed.Columns().AdjustToContents();
                worksheetDetailed.Rows().AdjustToContents();
            }

            return Workbook;
        }

        public IXLWorkbook ExportFullInfo()
        {
            ExportShortInfo();
            ExportDetailedInfo();

            return Workbook;
        }
        /*  public IXLWorkbook ExecuteExport()
          {
              for (int i = 0; i < Info.Count; i++)
              {

                  IXLWorksheet worksheet = Workbook.Worksheets.Add($"{Info[i].Group.GroupName}");

                  int column = 2;

                  worksheet.Cell(2, 1).Value = "Среднее количество коммитов за месяц:";
                  worksheet.Cell(3, 1).Value = "Всего коммитов за месяц:";
                  worksheet.Cell(4, 1).Value = "Котик месяца:";
                  worksheet.Cell(5, 1).Value = "Кандидат на ремень по жопе:";
                  worksheet.Row(1).SetDataType(XLDataType.Text);
                  foreach (var Stat in Info[i].Statistics)
                  {
                      worksheet.Cell(1, column).Value = Stat.Month;
                      worksheet.Cell(1, column).Style.DateFormat.Format = "MMMM-yyyy";
                      worksheet.Cell(2, column).Value = Stat.AverageValue;
                      worksheet.Cell(3, column).Value = Stat.TotalContributions;
                      worksheet.Cell(4, column).Value = Stat.MaxValueStudent.Item1.Username + ":" + Stat.MaxValueStudent.Item2;
                      worksheet.Cell(5, column).Value = Stat.MinValueStudent.Item1.Username + ":" + Stat.MinValueStudent.Item2;
                      column++;
                  }


                  worksheet.Columns().AdjustToContents();
                  worksheet.Rows().AdjustToContents();

                  IXLWorksheet worksheetDetailed = Workbook.Worksheets.Add($"{Info[i].Group.GroupName}-DetailedStat");

                  for (int row = 2; row < Info[i].Group.Students.Count + 2; row++)
                  {
                      worksheetDetailed.Cell(row, 1).Value = Info[i].Group.Students[row - 2].Username;
                  }

                  column = 2;

                  int finalColumn = 0;
                  int finalRow = 0;

                  foreach (var Stat in Info[i].Statistics)
                  {
                      worksheetDetailed.Cell(1, column).Value = Stat.Month;
                      worksheetDetailed.Cell(1, column).Style.DateFormat.Format = "MMMM-yyyy";
                      int row = 2;
                      int MonthSum = 0;
                      foreach (var studentResult  in Stat.DetailedStat)
                      {
                          worksheetDetailed.Cell(row, column).Value = studentResult.Item2;
                          MonthSum += studentResult.Item2;
                          finalRow = row;
                          row++;
                      }

                      worksheetDetailed.Cell(row, column).Value = MonthSum;
                      column++;
                      finalColumn = column;
                  }


                  for (int j = 2; j <= finalRow; j++)
                  {
                       int sum = 0;
                      for (int k = 2; k < finalColumn; k++)
                      {
                          sum += Convert.ToInt32(worksheetDetailed.Cell(j, k).Value);
                      }

                      worksheetDetailed.Cell(j, finalColumn).Value = sum;
                  }

                  worksheetDetailed.Columns().AdjustToContents();
                  worksheetDetailed.Rows().AdjustToContents();
              }
              return Workbook;
          }*/
    }
}
