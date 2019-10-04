using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZVSE_Scoreboard.Players;

namespace ZVSE_Scoreboard.Repositories
{

    public class MongoDb : IRepository
    {
        #region Essentials
        private readonly IMongoCollection<Player> collection;
        private readonly IMongoCollection<BsonDocument> bsonDocumentCollection;

        public MongoDb ( )
        {
            var mongoClient = new MongoClient ( "mongodb://localhost:27017" );
            var database = mongoClient.GetDatabase ( "Game" );
            this.collection = database.GetCollection<Player> ( "players" );
            this.bsonDocumentCollection = database.GetCollection<BsonDocument> ( "players" );
        }
        #endregion

        #region PlayerFunctions
        public async Task<Player> AddNewPlayer ( Player player )
        {
            await collection.InsertOneAsync ( player );
            return player;
        }

        public async Task<Player> DeletePlayer ( Guid id )
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq ( p => p.Id, id );
            return await collection.FindOneAndDeleteAsync ( filter );
        }

        public async Task<Player[]> GetAllPlayers ( )
        {
            var players = await collection.Find ( new BsonDocument ( ) ).ToListAsync ( );
            players.OrderByDescending ( p => p.Score );
            return players.ToArray ( );
        }

        public Task<Player> GetPlayerById ( Guid id )
        {
            var filter = Builders<Player>.Filter.Eq ( p => p.Id, id );
            return collection.Find ( filter ).FirstAsync ( );
        }

        public async Task<Player> GetPlayerByRank ( int rank )
        {
            var players = await collection.Find ( new BsonDocument ( ) ).ToListAsync ( );
            players.OrderByDescending ( p => p.Score );
            return players [ rank ];
        }

        public async Task<Player[]> GetTopByScore ( int take )
        {
            var players = await collection.Find ( new BsonDocument ( ) ). ToListAsync();       
            return players.OrderByDescending ( p => p.Score ).Take ( take ).ToArray();
        }

        public async Task<Player[]> GetTopBySurvival(int take)
        {
            var players = await collection.Find ( new BsonDocument ( ) ).ToListAsync ( );
            return players.OrderByDescending ( p => p.SurvivalTime ).Take ( take ).ToArray ( );
        }

        public async Task<Player[]> GetTopByLevel(int take)
        {
            var players = await collection.Find ( new BsonDocument ( ) ).ToListAsync ( );
            return players.OrderByDescending ( p => p.Level ).Take ( take ).ToArray ( );
        }

        public async Task<Player> ModifyPlayer ( Guid id )
        {
            Player player = new Player ( );
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq ( p => p.Id, id );
            UpdateDefinition<Player> update = Builders<Player>.Update.Set ( p => p.Score, player.Score );
            UpdateDefinition<Player> update1 = Builders<Player>.Update.Set ( p => p.Level, player.Level );
            UpdateDefinition<Player> update2 = Builders<Player>.Update.Set ( p => p.Name, player.Name );
            UpdateDefinition<Player> update3 = Builders<Player>.Update.Set ( p => p.SurvivalTime, player.SurvivalTime );      

            UpdateResult result = await collection.UpdateOneAsync ( filter, update );
            UpdateResult result1 = await collection.UpdateOneAsync ( filter, update1 );
            UpdateResult result2 = await collection.UpdateOneAsync ( filter, update2 );
            UpdateResult result3 = await collection.UpdateOneAsync ( filter, update3 );
            //return collection.AsQueryable ( ).First ( p => p.Id == id );
            return await collection.Find ( filter ).FirstAsync ( );
        }
        #endregion
    }
}
