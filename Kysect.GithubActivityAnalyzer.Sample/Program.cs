
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Aggregators;
using Kysect.GithubActivityAnalyzer.Aggregators.Models;
using Kysect.GithubActivityAnalyzer.ApiAccessor;
using Kysect.GithubActivityAnalyzer.Services;

namespace Kysect.GithubActivityAnalyzer.Sample
{
    internal class Program
    {
        static void Main()
        {
            var githubActivityProvider = new GithubActivityProvider();
            List<UserWithTag> grouped = ReadFromFile();

            List<Team> studyGroups = Team.CreateFromUserList(grouped, githubActivityProvider);

            var excelExportManager = new ExcelExportManager(studyGroups);
            excelExportManager.ExportFullInfo();
            excelExportManager.SaveExcel("data.xlsx");
        }

        private static List<UserWithTag> ReadFromFile()
        {
            List<UserWithTag> grouped = File
                .ReadLines("Data.txt")
                .Select(s =>
                {
                    string[] strings = s.Split("\t");
                    return new UserWithTag(strings[1], strings[0]);
                })
                .ToList();

            return grouped;
        }
    }
}
