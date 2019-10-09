using System;
using System.Collections.Generic;
using GameWebApi.Items;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GameWebApi.Players
{
    public class Player
    {
        [BsonId ( IdGenerator = typeof ( GuidGenerator ) )]
        [BsonRepresentation ( BsonType.String )]
        [BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsBanned { get; set; }
        public List<Item> Items = new List<Item> ( );
        public DateTime CreationTime { get; set; }
        public TagType tag { get; set; }

        public Player()
        {
            Id = Guid.NewGuid ( );
            CreationTime = DateTime.Now;
        }
    }

    public enum TagType
    {
        Dead,
        Alive,
        //...
    }
}
