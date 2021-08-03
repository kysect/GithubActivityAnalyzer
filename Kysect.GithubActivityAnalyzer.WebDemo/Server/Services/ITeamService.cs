using Kysect.GithubActivityAnalyzer.Data.Entities;
using Kysect.GithubActivityAnalyzer.WebDemo.Shared;
using System.Linq;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Services
{
    public interface ITeamService
    {
        public void AddTeam(Team team);
        public void DeleteTeam(string teamName);
        public void UpdateMember(string username, string teamName);
        public void DeleteMember(string username);

        //TODO: разобраться с возвращаемым значением
        public void GetAllTeams();
        public Team GetTeam(Team teamName);
    }
}
