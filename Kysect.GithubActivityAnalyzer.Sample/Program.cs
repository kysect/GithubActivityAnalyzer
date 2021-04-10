using System;
using System.Collections.Generic;
using System.Text.Json;
using Kysect.GithubActivityAnalyzer.Services;

namespace Kysect.GithubActivityAnalyzer.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GithubActivityProvider provider = new GithubActivityProvider();
            StudyGroup group = new StudyGroup("test");
            List<Student> students = new List<Student>()
            {
                new Student("FrediKats", provider),
                new Student("TomGnill", provider)
            };
            group.Students = students;
            Console.WriteLine(JsonSerializer.Serialize(group));
        }
    }
}
