using System;
using System.Threading.Tasks;
using Assignment3.Players;

namespace Assignment3.Repositories
{

    public interface IRepository
    {
        Task<Player> Get ( Guid id );
        Task<Player [ ]> GetAll ( );
        Task<Player> Create ( Player player );
        Task<Player> Modify ( Guid id, ModifiedPlayer player );
        Task<Player> Delete ( Guid id );
    }
}