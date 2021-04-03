using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using Kysect.GithubActivityAnalyzer.DetailedStats;

namespace Kysect.GithubActivityAnalyzer.ExcelExport
{
    public class ExportToExcel
    {
        public List<GroupInfo> Info { get; set; }

        public ExportToExcel(List<GroupInfo> info)
        {
            Info = info;
        }

        public void ExecuteExport(string path)
        {
            IXLWorkbook workbook = new XLWorkbook();

            for (int i = 0; i < Info.Count; i++)
            {

                IXLWorksheet worksheet = workbook.Worksheets.Add($"{Info[i].Group.GroupName}");

                int column = 2;

                worksheet.Cell(2, 1).Value = "Среднее количество коммитов за месяц:";
                worksheet.Cell(3, 1).Value = "Всего коммитов за месяц:";
                worksheet.Cell(4, 1).Value = "Котик месяца:";
                worksheet.Cell(5, 1).Value = "Кандидат на ремень по жопе:";
                worksheet.Row(1).SetDataType(XLDataType.Text);
                foreach (var Stat in Info[i].Statistics)
                {
                    worksheet.Cell(1, column).Value = Stat.Month.ToString("yyyy MMMM");
                    worksheet.Cell(2, column).Value = Stat.AverageValue;
                    worksheet.Cell(3, column).Value = Stat.TotalContributions;
                    worksheet.Cell(4, column).Value = Stat.MaxValueStudent.Item1.Username + ":" + Stat.MaxValueStudent.Item2;
                    worksheet.Cell(5, column).Value = Stat.MinValueStudent.Item1.Username + ":" + Stat.MinValueStudent.Item2;
                    column++;
                }


                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();

                IXLWorksheet worksheetDetailed = workbook.Worksheets.Add($"{Info[i].Group.GroupName}-DetailedStat");

                for (int row = 2; row < Info[i].Group.Students.Count + 2; row++)
                {
                    worksheetDetailed.Cell(row, 1).Value = Info[i].Group.Students[row - 2].Username;
                }

                column = 2;

                int finalColumn = 0;
                int finalRow = 0;

                foreach (var Stat in Info[i].Statistics)
                {
                    worksheetDetailed.Cell(1, column).Value = Stat.Month.ToString("yyyy MMMM");
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

            workbook.SaveAs(path);

        }
    }
}
