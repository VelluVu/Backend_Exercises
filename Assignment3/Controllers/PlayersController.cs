﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment3.Players;
using Assignment3.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment3.Controllers
{
    [Route ( "api/players" )]
    [ApiController]
    public class PlayersController : Controller
    {

        IRepository repo;

        // GET: /<controller>/
        public IActionResult Index ( )
        {
            return View ( );
        }

        public PlayersController ( IRepository repository )
        {
            repo = repository;
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
        [Route("{player:newplayer}")]
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
