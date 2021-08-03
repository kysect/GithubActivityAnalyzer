using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.WebDemo.Shared;
using Kysect.GithubActivityAnalyzer.Aggregators.Models;
using System.Net.Http.Json;
namespace Kysect.GithubActivityAnalyzer.WebDemo.Client.Pages
{
    public partial class UnitedStat
    {
        private string _username = String.Empty;
        private string _teamName = String.Empty;
        private List<string> _usernames = new List<string>();
        private TeamResponse _team;
        private bool _statsIsVisible = false;


        protected async Task GetStat()
        {
            Team newTeamInfo = new Team()
            {
                TeamName = _teamName,
                Usernames = _usernames
            };
            var response = await Http.PostAsJsonAsync("Team", newTeamInfo);
            _team = await response.Content.ReadFromJsonAsync<TeamResponse>();
            _statsIsVisible = true;
        }
        private void AddStudent()
        {
            if (!_usernames.Contains(_username))
                _usernames.Add(_username);
        }

    }
}
