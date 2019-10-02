using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.ErrorHandling;
using GameWebApi.Players;
using GameWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameWebApi.Controllers
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class PlayersController : Controller
    {

        private readonly IRepository repo;

        public PlayersController ( IRepository repo )
        {
            this.repo = repo;
        }

        // GET: /<controller>/     
        public IActionResult Index ( )
        {
            return View ( );
        }

        [HttpGet]
        [Route ( "{id}" )]
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
        [Route ( "{newPlayer}" )]
        [ShowMessageException ( typeof ( NotFoundException ) )]
        public async Task<Player> Create ( string newPlayer )
        {
            NewPlayer nPlayer = new NewPlayer ( );
            nPlayer.Name = newPlayer;
            return await repo.Create ( nPlayer );
        }

        [HttpPut]
        [Route ( "{id}/{score}" )]
        public async Task<Player> Modify ( string id, int score )
        {
            return await repo.Modify ( new Guid ( id ), new ModifiedPlayer { Score = score } );
        }

        [HttpDelete]
        [Route ( "{id}" )]
        public Task<Player> Delete ( string id )
        {
            return repo.Delete ( new Guid ( id ) );
        }
    }
}