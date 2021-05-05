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
        void ITeamService.GetAllTeams()
        {
            throw new System.NotImplementedException();
        }
        IQueryable<Member> ITeamService.GetTeam(string teamName)
        {
            throw new System.NotImplementedException();
        }
    }
}
