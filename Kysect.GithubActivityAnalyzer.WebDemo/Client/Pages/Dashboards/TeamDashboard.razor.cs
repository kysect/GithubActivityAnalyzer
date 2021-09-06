using System;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.WebDemo.Shared;
using Kysect.GithubActivityAnalyzer.Aggregators.Models;
using Microsoft.JSInterop;
using System.Net;
using System.Net.Http.Json;
using System.Collections.Generic;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Client.Pages.Dashboards
{
    public partial class TeamDashboard
    {
        private string _teamName = String.Empty;
        private TeamResponse _team;
        private bool _statsIsVisible = false;
        private bool _errorMessageVisible = false;
        private List<ShortMemberInfo> _teamInfo;

        private async Task GenerateBarChart()
        {
            await GetStat();
            StateHasChanged();
            if (_statsIsVisible)
            {
                _teamInfo = _team.Members;
            }
        }
        private async Task GetStat()
        {
            Team team = await GetTeam();
            if (team is not null)
            {
                var teamInfoResponse = await Http.PostAsJsonAsync("TeamInfo/GetTeamInfo", team);
                _team = await teamInfoResponse.Content.ReadFromJsonAsync<TeamResponse>();
                _statsIsVisible = true;
            }
            else
            {
                _statsIsVisible = false;
                _errorMessageVisible = true;
            }

        }
        private async Task<Team> GetTeam()
        {
            var teamResponse = await Http.PostAsJsonAsync("DBTeam/GetTeam", new Team(_teamName));
            if (teamResponse.StatusCode == HttpStatusCode.OK)
            {
                return await teamResponse.Content.ReadFromJsonAsync<Team>();
            }
            else
            {
                return null;
            }
        }

    }
}
