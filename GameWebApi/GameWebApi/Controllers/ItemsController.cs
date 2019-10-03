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

        [HttpPost]
        [Route ( "{newItem}" )]
        [ShowMessageException ( typeof ( NotFoundException ) )]
        public Task<Item> CreateItem ( string playerId, NewItem newItem )
        {
            logger.LogInformation ( "Creating item with name " + newItem.Name );
            Item item = new Item ( );
            item.Name = newItem.Name;
            return repo.CreateItem ( new Guid ( playerId ), item );
        }

        [HttpPut]
        [Route ( "{itemId}/{modifiedItem}" )]
        public Task<Item> ModifyItem ( string playerId, string itemId, ModifiedItem modifiedItem )
        {
            return repo.ModifyItemAsync ( new Guid ( playerId ), new Guid ( itemId ), new Item ( ) {  Level = modifiedItem.Level } );
        }

        [HttpDelete]
        [Route ( "{itemId}" )]
        public Task<Item> DeleteItem ( string playerId, string itemId )
        {
            return repo.DeleteItemAsync ( new Guid ( playerId ), new Guid ( itemId ) );
        }
    }
}
