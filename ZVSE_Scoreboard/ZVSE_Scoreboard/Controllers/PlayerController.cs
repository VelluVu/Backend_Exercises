using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZVSE_Scoreboard.Players;
using ZVSE_Scoreboard.Repositories;

namespace ZVSE_Scoreboard.Controllers
{
    [Route ( "api/players" )]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly ILogger<PlayerController> logger;
        private readonly IRepository repo;

        public PlayerController ( ILogger<PlayerController> logger, IRepository repo )
        {
            this.logger = logger;
            this.repo = repo;
        }

        public IActionResult Index ( )
        {
            return View ( );
        }

        [HttpPost ( "{name:alpha}" )]
        public Task<Player> AddNewPlayer ( string name )
        {
            return repo.AddNewPlayer ( new Player { Name = name } );
        }

        [HttpGet ( "{id:alpha}" )]
        public Task<Player> GetPlayerById ( string id )
        {
            return repo.GetPlayerById ( new Guid(id) );
        }

        [HttpGet ( "{rank:int}" )]
        public Task<Player> GetPlayerByRank ( int rank )
        {
            return repo.GetPlayerByRank ( rank );
        }

        [HttpGet ( "topscore/{take:int:min(1):max(20)}" )]
        public Task<Player[]> GetTopScore ( int take )
        {
            return repo.GetTopByScore ( take );
        }

        [HttpGet ( "toplevel/{take:int:min(1):max(20)}" )]
        public Task<Player [ ]> GetTopLevel ( int take )
        {
            return repo.GetTopByLevel ( take );
        }

        [HttpGet ( "topsurvival/{take:int:min(1):max(20)}" )]
        public Task<Player [ ]> GetTopSurvival ( int take )
        {
            
            return repo.GetTopBySurvival ( take );
        }

        [HttpGet ( "" )]
        public Task<Player[]> GetAllPlayers ( )
        {
            return repo.GetAllPlayers ( );
        }

        [HttpPut ( "" )]
        public Task<Player> ModifyPlayer ( Player player )
        {          
            return repo.ModifyPlayer ( player.Id, player );
        }

        [HttpDelete ( "{id:alpha}" )]
        public Task<Player> DeletePlayer ( string id )
        {
            return repo.DeletePlayer ( new Guid ( id ) );
        }
    }
}