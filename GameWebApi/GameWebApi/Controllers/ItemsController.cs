using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameWebApi.Items;
using GameWebApi.Players;
using GameWebApi.Repositories;
using GameWebApi.ErrorHandling;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameWebApi.Controllers
{
    [Route ( "api/players/{playerId}/items" )]
    [ApiController]
    public class ItemsController : Controller
    {

        private readonly IRepository repo;

        public ItemsController ( IRepository repo )
        {
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
            return repo.GetAllItems ( new Guid ( playerId ) );
        }

        [HttpPost]
        [Route ( "{itemName}" )]
        [ShowMessageException ( typeof ( NotFoundException ) )]
        public Task<Item> CreateItem ( string playerId, string itemName )
        {
            NewItem item = new NewItem ( );
            item.Name = itemName;
            return repo.CreateItem ( new Guid ( playerId ), item );
        }

        [HttpPut]
        [Route ( "{id}/{item}" )]
        public Task<Item> ModifyItem ( string playerId, string itemId, ModifiedItem item )
        {
            return repo.ModifyItem ( new Guid ( playerId ), new Guid ( itemId ), item );
        }

        [HttpDelete]
        [Route ( "{id}" )]
        public Task<Item> DeleteItem ( string playerId, string itemId )
        {
            return repo.DeleteItem ( new Guid ( playerId ), new Guid ( itemId ) );
        }
    }
}
