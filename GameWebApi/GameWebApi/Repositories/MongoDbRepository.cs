using GameWebApi.Items;
using GameWebApi.Players;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameWebApi.Repositories
{
    public class MongoDbRepository : IRepository
    {
        #region Essentials
        private readonly IMongoCollection<Player> collection;
        private readonly IMongoCollection<BsonDocument> bsonDocumentCollection;

        public MongoDbRepository ( )
        {
            var mongoClient = new MongoClient ( "mongodb://localhost:27017" );
            var database = mongoClient.GetDatabase ( "game" );
            this.collection = database.GetCollection<Player> ( "players" );
            this.bsonDocumentCollection = database.GetCollection<BsonDocument> ( "players" );
        }
        #endregion

        #region PlayerDBQueries
        public async Task<Player> Create ( Player player )
        {
            await collection.InsertOneAsync ( player );
            return player;
        }

        public async Task<Player> Delete ( Guid playerId )
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq ( p => p.Id, playerId );
            return await collection.FindOneAndDeleteAsync ( filter );
        }

        public async Task<Player> Modify ( Guid playerId, Player player )
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq ( p => p.Id, playerId );
            UpdateDefinition<Player> update = Builders<Player>.Update.Set ( p => p.Score, player.Score );
            UpdateResult result = await collection.UpdateOneAsync ( filter, update );
            //return collection.AsQueryable ( ).First ( p => p.Id == id );
            return await collection.Find ( filter ).FirstAsync ( );
        }

        public Task<Player> Get ( Guid id )
        {
            var filter = Builders<Player>.Filter.Eq ( p => p.Id, id );
            return collection.Find ( filter ).FirstAsync ( );
        }

        public Task<Player> GetByName( string name)
        {
            var filter = Builders<Player>.Filter.Eq ( p => p.Name, name );
            return collection.Find ( filter ).FirstAsync ( );
        }

        public async Task<Player [ ]> GetAll ( )
        {   
            var players = await collection.Find ( new BsonDocument ( ) ).ToListAsync ( );        
            return players.ToArray ( );
        }

        public async Task<Player[]> GetPlayersWithTag(TagType tag)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq ( p => p.tag, tag );
            var players = await collection.Find ( filter ).ToListAsync ( );
            return players.ToArray ( );
        }

        public async Task<Player[]> GetByScore ( int min )
        {
            //var players = await collection.Find ( new BsonDocument ( ) ).ToListAsync ( );
            //players.OrderByDescending ( p => p.Score > min );

            FilterDefinition<Player> filter = Builders<Player>.Filter.Gte ( p => p.Score, min );
            var players = await collection.Find ( filter ).ToListAsync ( );
            return players.ToArray ( );
        }

        //public async Task<Player[]> GetPlayersWithItemProperty()
        //{

        //}
        #endregion

        #region ItemDBQueries
        public async Task<Item> CreateItem ( Guid playerId, Item item )
        {
            var filter = Builders<Player>.Filter.Eq ( p => p.Id, playerId );
            var update = Builders<Player>.Update.AddToSet ( p=> p.itemList, item );
            await collection.FindOneAndUpdateAsync ( filter, update );
            return item;
        }

        public async Task<Item [ ]> GetAllItemsAsync ( Guid playerId )
        {
            var filter = Builders<Player>.Filter.Eq ( p => p.Id, playerId );
            var player = ( await collection.FindAsync ( filter ) ).Single ( );
            return player.itemList.ToArray ( );
        }

        public async Task<Item> GetItem ( Guid playerId, Guid itemId )
        {
            var filter = Builders<Player>.Filter.Eq ( p => p.Id, playerId );
            var player = ( await collection.FindAsync ( filter ) ).Single ( );
            return GetItemByID ( player, itemId );
        }

        public async Task<Item> ModifyItemAsync ( Guid playerId, Guid itemId, Item item )
        {
            var filter = Builders<Player>.Filter.Where ( p => p.Id == playerId && p.itemList.Any ( it => it.Id == itemId ) );
            var update = Builders<Player>.Update.Set ( p => p.itemList [ -1 ].Level, item.Level );

            var player = await collection.FindOneAndUpdateAsync ( filter, update );

            var i = GetItemByID ( player, itemId );
            i.Level = item.Level;

            return i;
        }

        public async Task<Item> DeleteItemAsync ( Guid playerId, Guid itemId )
        {
            var filter = Builders<Player>.Filter.Eq ( p => p.Id, playerId );
            var itemFilter = Builders<Item>.Filter.Eq ( i => i.Id, itemId );
            var update = Builders<Player>.Update.PullFilter ( p => p.itemList, itemFilter );

            var player = await collection.FindOneAndUpdateAsync ( filter, update );
            return GetItemByID ( player, itemId );
        }

        private Item GetItemByID ( Player player, Guid itemId )
        {
            foreach ( Item playerItem in player.itemList )
            {
                if ( playerItem.Id == itemId )
                {
                    return playerItem;
                }
            }

            throw new ErrorHandling.NotFoundException("Item was not found");

        }
       
        #endregion
    }
}
