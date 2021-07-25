using System.Collections.Generic;
using ClosedXML.Excel;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Aggregators;

namespace Kysect.GithubActivityAnalyzer.Extensions.Services
{
    public class ExcelExportManager
    {
        public List<Team> Info { get; set; }
        private IXLWorkbook Workbook { get; }

        public ExcelExportManager(List<Team> info)
        {
            Info = info;
            Workbook = new XLWorkbook();
        }
        public ExcelExportManager(List<Team> info, IXLWorkbook workbook)
        {
            Info = info;
            Workbook = workbook;
        }

        public void SaveExcel(string path)
        {
            Workbook.SaveAs(path);
        }

        public IXLWorkbook ExportShortInfo()
        {
            foreach (var teamInfo in Info)
            {
                IXLWorksheet worksheet = Workbook.Worksheets.Add($"{teamInfo.TeamName}");
                worksheet.Cell(2, 1).Value = "Среднее количество коммитов за месяц:";
                worksheet.Cell(3, 1).Value = "Всего коммитов за месяц:";
                worksheet.Cell(4, 1).Value = "Котик месяца:";
                worksheet.Cell(5, 1).Value = "Кандидат на ремень по жопе:";
                worksheet.Row(1).SetDataType(XLDataType.Text);
                for (int column = 2; column < teamInfo.Statistics.Count + 2; column++)
                {
                    worksheet.Cell(1, column).Value = teamInfo.Statistics[column - 2].Month;
                    worksheet.Cell(1, column).Style.DateFormat.Format = "MMMM-yyyy";
                    worksheet.Cell(2, column).Value = teamInfo.Statistics[column - 2].AverageValue;
                    worksheet.Cell(3, column).Value = teamInfo.Statistics[column - 2].TotalContributions;
                    worksheet.Cell(4, column).Value =
                        teamInfo.Statistics[column - 2].MaxValueMember.Username + ":" + teamInfo.Statistics[column - 2].MaxValueMember.MonthlyContributions;
                    worksheet.Cell(5, column).Value =
                        teamInfo.Statistics[column - 2].MinValueMember.Username + ":" + teamInfo.Statistics[column - 2].MinValueMember.MonthlyContributions;
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
                IXLWorksheet worksheetDetailed = Workbook.Worksheets.Add($"{info.TeamName}-DetailedStat");

                for (int row = 2; row < info.Members.Count + 2; row++)
                {
                    worksheetDetailed.Cell(row, 1).Value = info.Members[row - 2].Username;
                }
                int column = 2;

                int lastColumn = 0;
                int lastRow = 0;

                foreach (var stat in info.Statistics)
                {
                    worksheetDetailed.Cell(1, column).Value = stat.Month;
                    worksheetDetailed.Cell(1, column).Style.DateFormat.Format = "MMMM-yyyy";
                    int row = 2;
                    foreach (var studentResult in stat.DetailedStat)
                    {
                        worksheetDetailed.Cell(row, column).Value = studentResult.MonthlyContributions;
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
                    IXLAddress address2 = worksheetDetailed.Cell(j, lastColumn - 1).Address;
                    var cellWithFormula = worksheetDetailed.Cell(j, lastColumn);
                    cellWithFormula.FormulaA1 = $"=SUM({address1}:{address2})";
                }
                worksheetDetailed.Columns().AdjustToContents();
                worksheetDetailed.Rows().AdjustToContents();
            }

            return Workbook;
        }
        public IXLWorkbook ExportSummaryInfo()
        {
            IXLWorksheet worksheetSummary = Workbook.Worksheets.Add($"Summary");
            int month = 0;
            for (int col = 2; col < Info[0].Statistics.Count + 2; col++)
            {
                worksheetSummary.Cell(1, col).Value = Info[0].Statistics[month].Month;
                worksheetSummary.Cell(1, col).Style.DateFormat.Format = "MMMM-yyyy";
                month++;
            }

            int row = 2;
            int column = 2;
            foreach (var info in Info)
            {
                worksheetSummary.Cell(row, 1).Value = info.TeamName;

                foreach (var statistic in info.Statistics)
                {
                    worksheetSummary.Cell(row, column).Value = statistic.DetailedStat.Sum(a => a.MonthlyContributions);
                    column++;
                }

                column = 2;
                row++;
            }
            worksheetSummary.Columns().AdjustToContents();
            worksheetSummary.Rows().AdjustToContents();
            return Workbook;
        }

        public IXLWorkbook ExportFullInfo()
        {
            ExportSummaryInfo();
            ExportShortInfo();
            ExportDetailedInfo();
            return Workbook;
        }
    }
}
