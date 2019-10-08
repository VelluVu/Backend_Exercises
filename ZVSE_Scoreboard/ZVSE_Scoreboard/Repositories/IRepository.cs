using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZVSE_Scoreboard.Players;

namespace ZVSE_Scoreboard.Repositories
{
    public interface IRepository
    {
        Task<Player> AddNewPlayer ( Player player );
        Task<Player> GetPlayerById ( Guid id );
        Task<Player> GetPlayerByRank ( int rank );   
        Task<Player> ModifyPlayer ( Guid id, ModifiedPlayer player );
        Task<Player> DeletePlayer ( Guid id );

        Task<Player [ ]> GetAllPlayers ( );
        Task<Player [ ]> GetTopByScore ( int take );        
        Task<Player [ ]> GetTopByLevel ( int take );
        Task<Player [ ]> GetTopBySurvival ( int take );
    }
}
