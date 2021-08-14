using Kysect.GithubActivityAnalyzer.WebDemo.Shared;
using Kysect.GithubActivityAnalyzer.Data.Repositories;
using Kysect.GithubActivityAnalyzer.Data.Entities;
using System.Linq;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Services
{
    public class TeamService : ITeamService
    {
        private readonly TeamRepository _teamRepository;
        public TeamService(TeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public void AddTeam(Team newTeam) 
        {
            foreach (var username in newTeam.Usernames)
            {
                _teamRepository.Create(new Member() { Username = username, Team = newTeam.TeamName });
            }
        }
        public void DeleteTeam(string teamName)
        {
            _teamRepository.DeleteByTeam(teamName);
        }
        public void UpdateMember(string username, string teamName)
        {
            _teamRepository.Update(new Member { Team = teamName, Username = username });
        }
        public void DeleteMember(string username)
        {
            _teamRepository.DeleteByUsername(username);
        }

        //TODO: Уточнить с ТЗ и реализовать
        public void GetAllTeams()
        {
            throw new System.NotImplementedException();
        }
        public bool TryGetTeam(Team teamName, out Team team)
        {
            team = new Team() { TeamName = teamName.TeamName };
            team.Usernames = _teamRepository.GetAllByTeam(teamName.TeamName).Select(p => p.Username).ToList();
            if (team.Usernames.Any())
                return true;
            else
                return false;
        }
    }
}
