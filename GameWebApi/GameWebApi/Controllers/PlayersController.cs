using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.ErrorHandling;
using GameWebApi.Players;
using GameWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameWebApi.Controllers
{
    [Route ( "api/players" )]
    [ApiController]
    public class PlayersController : Controller
    {
        private readonly ILogger<PlayersController> logger;
        private readonly IRepository repo;

        public PlayersController ( ILogger<PlayersController> logger,IRepository repo )
        {
            this.logger = logger;
            this.repo = repo;
        }

        // GET: /<controller>/     
        public IActionResult Index ( )
        {
            return View ( );
        }

        [HttpGet]
        [Route ( "{id:alpha}" )]
        public Task<Player> Get ( string id )
        {
            return repo.Get ( new Guid ( id ) );
        }

        [HttpGet]
        [Route ( "" )]
        public Task<Player [ ]> GetAll ( )
        {
            return repo.GetAll ( );
        }

        [HttpPost]
        [Route ("")]
        [ShowMessageException ( typeof ( NotFoundException ) )]
        public async Task<Player> Create ( NewPlayer newPlayer )
        {
            logger.LogInformation ( "Creating player with name " + newPlayer.Name );
            Player nPlayer = new Player ( );
            nPlayer.Name = newPlayer.Name;
            return await repo.Create ( nPlayer );
        }

        [HttpPut]
        [Route ( "{id:alpha}/{score:int}" )]
        public async Task<Player> Modify ( string id, int score )
        {
            return await repo.Modify ( new Guid ( id ), new Player { Score = score } );
        }

        [HttpDelete]
        [Route ( "{id:alpha}" )]
        public Task<Player> Delete ( string id )
        {
            return repo.Delete ( new Guid ( id ) );
        }
    }
}