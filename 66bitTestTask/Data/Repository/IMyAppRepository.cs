using _66bitTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace _66bitTestTask.Data.Repository
{
    public interface IMyAppRepository
    {
        public Task AddSoccerPlayer(SoccerPlayer player);
        public Task AddTeam(Team team);
        public Task<Team?> GetTeamByName(string name);

        public Task<Team[]> GetAllTeams();

        public Task<SoccerPlayer[]> GetAllSoccerPlayers();

        public Task<SoccerPlayer> GetSoccerPlayerById(Guid id);

        public Task UpdatePlayerById(Guid id, SoccerPlayer newPlayerValues);
    }
}
