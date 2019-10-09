using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameWebApi.Items;
using GameWebApi.Players;
using GameWebApi.Repositories;
using GameWebApi.ErrorHandling;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameWebApi.Controllers
{
    [Route ( "api/players/{playerId:alpha}/items" )]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly ILogger<ItemsController> logger;
        private readonly IRepository repo;

        public ItemsController ( ILogger<ItemsController> logger, IRepository repo )
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
        [Route ( "{itemId:alpha}" )]
        public Task<Item> GetItem ( string playerId, string itemId )
        {

            return repo.GetItem ( new Guid ( playerId ), new Guid ( itemId ) );
        }

        [HttpGet]
        [Route ( "" )]
        public Task<Item [ ]> GetAllItems ( string playerId )
        {
            return repo.GetAllItemsAsync ( new Guid ( playerId ) );
        }

        //[ShowMessageException ( typeof ( NotFoundException ) )]
        [HttpPost]
        [Route ( "{name:alpha}" )]     
        public Task<Item> CreateItem ( string playerId, string name )
        {
            logger.LogInformation ( "Creating item with name " + name );
            Item item = new Item ( );
            item.Name = name;
            return repo.CreateItem ( new Guid ( playerId ), item );
        }

        [HttpPut ( "/api/players/{playerId}/items/AddItemToPlayer/" )]
        public async Task<Item> AddItemToPlayer ( Guid playerId, Item item )
        {
            item.Id = Guid.NewGuid ( );
            item.CreationTime = DateTime.UtcNow;
            return await repo.AddItemToPlayer( playerId, item );
        }

        [HttpPut]
        [Route ( "{itemId:alpha}/{level:int}" )]
        public Task<Item> ModifyItem ( string playerId, string itemId, int level )
        {
            return repo.ModifyItemAsync ( new Guid ( playerId ), new Guid ( itemId ), new Item ( ) {  Level = level } );
        }

        [HttpDelete]
        [Route ( "{itemId:alpha}" )]
        public Task<Item> DeleteItem ( string playerId, string itemId )
        {
            return repo.DeleteItemAsync ( new Guid ( playerId ), new Guid ( itemId ) );
        }

        [HttpDelete ( "/api/players/{playerId}/items/sell/{itemId}" )]
        public async Task<Player> SellItem ( Guid playerId, Guid itemId )
        {
            return await repo.SellItem ( playerId, itemId );
        }
    }
}
