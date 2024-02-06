using _66bitTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace _66bitTestTask.Data.Repository
{
    public class MyAppRepository : IMyAppRepository
    {
        private readonly MyAppContext context;

        public MyAppRepository(MyAppContext context)
        {
            this.context = context;
        }

        public async Task AddSoccerPlayer(SoccerPlayer player)
        {
            var entry = context.Entry(player);
            if (entry.State == EntityState.Detached)
                await context.SoccerPlayers.AddAsync(player);
            await context.SaveChangesAsync();
        }

        public async Task AddTeam(Team team)
        {
            var entry = context.Entry(team);
            if (entry.State == EntityState.Detached)
                await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePlayerById(Guid id,SoccerPlayer newPlayer)
        {
            var player = await GetSoccerPlayerById(id);
            player.Team = newPlayer.Team;
            player.FirstName = newPlayer.FirstName;
            player.LastName = newPlayer.LastName;
            player.Gender = newPlayer.Gender;
            player.Country = newPlayer.Country;
            player.BirthDate = newPlayer.BirthDate;
            await context.SaveChangesAsync();
        }

        public async Task<SoccerPlayer[]> GetAllSoccerPlayers()
        {
            return await context.SoccerPlayers.Include(x => x.Team).ToArrayAsync();
        }

        public async Task<Team[]> GetAllTeams()
        {
            return await context.Teams.ToArrayAsync();
        }

        public async Task<SoccerPlayer?> GetSoccerPlayerById(Guid id)
        {
            return await context.SoccerPlayers.FindAsync(id);
        }

        public async Task<Team?> GetTeamByName(string name)
        {
            return await context.Teams.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
