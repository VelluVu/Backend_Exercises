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
    [Route ( "api/players/{playerId}/items" )]
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
        [Route ( "{itemId}" )]
        public Task<Item> GetItem ( Guid playerId, Guid itemId )
        {

            return repo.GetItem ( playerId , itemId );
        }

        [HttpGet]
        [Route ( "" )]
        public Task<Item [ ]> GetAllItems ( Guid playerId )
        {
            return repo.GetAllItemsAsync ( playerId );
        }

        //[ShowMessageException ( typeof ( NotFoundException ) )]
        [HttpPost]
        [Route ( "{name}" )]     
        public Task<Item> CreateItem ( Guid playerId, NewItem newItem )
        {
            logger.LogInformation ( "Creating item with name " + newItem.Name );
            Item item = new Item ( );
            item.Name = newItem.Name;
            return repo.CreateItem (playerId, item );
        }

        [HttpPut ( "/api/players/{playerId}/items/AddItemToPlayer/" )]
        public async Task<Item> AddItemToPlayer ( Guid playerId, Item item )
        {
            item.Id = Guid.NewGuid ( );
            item.CreationTime = DateTime.UtcNow;
            return await repo.AddItemToPlayer( playerId, item );
        }

        [HttpPut]
        [Route ( "{itemId}/{level:int}" )]
        public Task<Item> ModifyItem ( Guid playerId, Guid itemId, int level )
        {
            return repo.ModifyItemAsync ( playerId, itemId , new Item ( ) {  Level = level } );
        }

        [HttpDelete]
        [Route ( "{itemId:alpha}" )]
        public Task<Item> DeleteItem ( Guid playerId, Guid itemId )
        {
            return repo.DeleteItemAsync ( playerId, itemId );
        }

        [HttpDelete ( "/api/players/{playerId}/items/sell/{itemId}" )]
        public async Task<Player> SellItem ( Guid playerId, Guid itemId )
        {
            return await repo.SellItem ( playerId, itemId );
        }
    }
}
