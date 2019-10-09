using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.CustomAttributes;
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

        public PlayersController ( ILogger<PlayersController> logger, IRepository repo )
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

        [HttpGet ("name/{name:alpha}")] 
        public Task<Player> GetByName(string name)
        {
            return repo.GetByName ( name );
        }

        [HttpGet]
        [Route ( "" )]
        public Task<Player [ ]> GetAll ( )
        {
            return repo.GetAll ( );
        }

        [HttpGet]
        [Route ( "tag/{tag}" )]
        public Task<Player [ ]> GetAllWithTag ( TagType tag )
        {
            Type enumType = tag.GetType ( );
            bool isEnumValid = Enum.IsDefined ( enumType, tag );
            if ( !isEnumValid )
            {
                throw new Exception ( "Not Valid Enum" );
            }
            return repo.GetPlayersWithTag ( tag );
        }

        [HttpGet( "{minScore:int:min(1)}" )]
        public Task<Player[]> GetByScore(int minScore )
        {
            return repo.GetByScore ( minScore );
        }

        [HttpGet("topscore/")]
        public Task<Player [ ]> GetTop10Score ( )
        {
            //var players = await collection.Find ( new BsonDocument ( ) ).ToListAsync ( );
            //players.OrderByDescending ( p => p.Score > min );

            return repo.GetTop10Score ( );
        }

        [HttpGet ( "score/avg/{start}/{end}")]
        public Task<int> AverageScoreBetweenDates ( DateTime start, DateTime end )
        {
            return repo.AverageScoreBetweenDates ( start, end );       
        }

        [HttpGet ( "/api/players/withitem/{type}" )]
        public async Task<Player [ ]> GetPlayersWithItems ( Items.ItemType type )
        {
            return await repo.GetPlayersWithItemType ( type );
        }

        [HttpGet ( "/api/players/withitemamount/{amount}" )]
        public async Task<Player [ ]> GetPlayersWithItemAmount ( int amount )
        {
            return await repo.GetPlayersWithAmountOfItems ( amount );
        }

        //[ShowMessageException ( typeof ( NotFoundException ) )]
        [HttpPost]
        [Route ( "" )]
        [ValidateModel]
        public async Task<Player> Create ( [FromBody] NewPlayer newPlayer )
        {
            logger.LogInformation ( "Creating player with name " + newPlayer.Name );
            Player nPlayer = new Player ( );
            nPlayer.Name = newPlayer.Name;
            return await repo.Create ( nPlayer );
        }

        [HttpPut]
        [Route ( "{id:alpha}" )]
        public Task<Player> Modify ( string id, [FromBody] ModifiedPlayer score )
        {
            return repo.Modify ( new Guid ( id ), score);
        }

        [HttpPut]
        [Route ( "{id:alpha}/score" )]
        public Task IncrementScore ( string id, [FromBody] AddScore addScore )
        {
            return repo.IncrementScore ( new Guid ( id ), addScore );
        }

        [HttpPut]
        [Route ( "{id:alpha}/name" )]
        public Task ChangeName ( string id, UpdateName updateName )
        {
            return repo.ChangeName ( new Guid(id), updateName );
        }

        [HttpDelete]
        [Route ( "{id:alpha}" )]
        public Task<Player> Delete ( string id )
        {
            return repo.Delete ( new Guid ( id ) );
        }
    }
}