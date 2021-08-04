using System;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.WebDemo.Shared;
using Kysect.GithubActivityAnalyzer.Aggregators.Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using System.Linq;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Client.Pages.Dashboards
{
    public partial class TeamDashboard
    {
        private string _teamName = String.Empty;
        private TeamResponse _team;
        private bool _statsIsVisible = true;
        private async Task GetStat()
        {
            var team = await Http.PostAsJsonAsync("DBTeam/GetTeam", new Team(_teamName));
            Team teamToAggregate = await team.Content.ReadFromJsonAsync<Team>();
            if (teamToAggregate.Usernames.Any())
            {
                var teamResponse = await Http.PostAsJsonAsync("Team", teamToAggregate);
                _team = await teamResponse.Content.ReadFromJsonAsync<TeamResponse>();
                _statsIsVisible = true;
            }
            else { _statsIsVisible = false;}
        }
        private async void GenerateBarChart()
        {
            await GetStat();
            StateHasChanged();
            if (_statsIsVisible)
            {
                var teamInfo = _team.Members;
                await jsRuntime.InvokeVoidAsync("GenerateBarChart", teamInfo);
            }
        }
    }
}
