using System;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.WebDemo.Shared;
using Kysect.GithubActivityAnalyzer.Aggregators.Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Client.Pages.Dashboards
{
    public partial class TeamDashboard
    {
        private string _teamName = String.Empty;
        private TeamResponse _team;
        private bool _statsIsVisible = false;
        private async Task GetStat()
        {
            var teamResponse = await Http.PostAsJsonAsync("DBTeam/GetTeam", new Team(_teamName));
            Team teamInfo = await teamResponse.Content.ReadFromJsonAsync<Team>();
            var response = await Http.PostAsJsonAsync("Team", teamInfo);
            _team = await response.Content.ReadFromJsonAsync<TeamResponse>();
            _statsIsVisible = true;
        }
        private async void GenerateBarChart()
        {
            await GetStat();
            var teamInfo = _team.Members;
            await jsRuntime.InvokeVoidAsync("GenerateBarChart", teamInfo);
        }

    }
}
