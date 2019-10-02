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
        Task<Player> Get ( Guid id );
        Task<Player [ ]> GetAll ( );
        Task<Player> Create ( NewPlayer player );
        Task<Player> Modify ( Guid id, ModifiedPlayer player );
        Task<Player> Delete ( Guid id );

        Task<Item> GetItem ( Guid playerI, Guid id );
        Task<Item [ ]> GetAllItems ( Guid player );
        Task<Item> CreateItem ( Guid player, NewItem item );
        Task<Item> ModifyItem ( Guid player, Guid id, ModifiedItem item );
        Task<Item> DeleteItem ( Guid player, Guid id );
    }
}
