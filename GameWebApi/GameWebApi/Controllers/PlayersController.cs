using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Players;
using GameWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameWebApi.Controllers
{
    [Route ( "api/players/" )]
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
        [Route ( "{id:guid}" )]
        public Task<Player> Get ( Guid id )
        {
            return repo.Get ( id );
        }

        [HttpGet]
        [Route ( "" )]
        public Task<Player [ ]> GetAll ( )
        {
            return repo.GetAll ( );
        }

        [HttpPut]
        [Route ( "{player:newplayer}" )]
        public Task<Player> Create ( NewPlayer player )
        {
            Player newPlayer = new Player ( );
            newPlayer.Id = new Guid ( );
            newPlayer.Name = player.Name;
            return repo.Create ( newPlayer );
        }

        [HttpPut]
        [Route ( "{id:guid}{player:modifiedplayer}" )]
        public Task<Player> Modify ( Guid id, ModifiedPlayer player )
        {
            return repo.Modify ( id, player );
        }

        [HttpDelete]
        [Route ( "{id:guid}" )]
        public Task<Player> Delete ( Guid id )
        {
            return repo.Delete ( id );
        }
    }
}