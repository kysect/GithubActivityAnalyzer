using Kysect.GithubActivityAnalyzer.WebDemo.Shared;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Services
{
    public interface ITeamService
    {
        void AddTeam(Team team);
        void DeleteTeam(string teamName);
        void UpdateMember(string username, string teamName);
        void DeleteMember(string username);

        //TODO: разобраться с возвращаемым значением
        void GetAllTeams();
        bool TryGetTeam(Team teamName, out Team team);
    }
}
