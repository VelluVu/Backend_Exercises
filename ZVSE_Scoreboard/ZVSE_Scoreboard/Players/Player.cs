using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZVSE_Scoreboard.Players
{
    public class Player
    {
        [BsonId ( IdGenerator = typeof ( GuidGenerator ) )]
        [BsonRepresentation ( BsonType.String )]
        [BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public float SurvivalTime { get; set; }
        public DateTime CreationTime { get; set; }

        public Player()
        {
            Id = Guid.NewGuid ( );
            CreationTime = DateTime.Now;

        }
    }
}
