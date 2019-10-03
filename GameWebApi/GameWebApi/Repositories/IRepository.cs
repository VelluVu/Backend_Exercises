using GameWebApi.Items;
using GameWebApi.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Repositories
{
    public interface IRepository
    {
        Task<Player> Get ( Guid playerId );
        Task<Player [ ]> GetAll ( );
        Task<Player> Create ( Player player );
        Task<Player> Modify ( Guid playerId, Player player );
        Task<Player> Delete ( Guid playerId );

        Task<Item> GetItem ( Guid playerId, Guid itemId );
        Task<Item [ ]> GetAllItemsAsync ( Guid playerId );
        Task<Item> CreateItem ( Guid playerId, Item itemId );
        Task<Item> ModifyItemAsync ( Guid playerId, Guid itemId, Item item );
        Task<Item> DeleteItemAsync ( Guid playerId, Guid itemId );
    }
}
