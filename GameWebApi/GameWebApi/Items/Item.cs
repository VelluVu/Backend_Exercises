﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Items
{
    public class Item
    {
        [BsonId ( IdGenerator = typeof ( GuidGenerator ) )]
        [BsonRepresentation ( BsonType.String )]
        [BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        [StringLength ( 128 )]
        public string Name { get; set; }
        [Range ( 1, 99 )]
        public int Level { get; set; }
        [AllowedItemTypes ( ItemType.Potion, ItemType.Shield, ItemType.Sword )]
        public ItemType Type { get; set; }
        [DateFromPast]
        [DataType ( DataType.Date )]
        public DateTime CreationTime { get; set; }
        public object Value { get; internal set; }

        public Item ( )
        {
            Id = Guid.NewGuid ( );
            CreationTime = DateTime.Now;
        }

    }

    public enum ItemType
    {
        Sword,
        Potion,
        Shield,
    }
}
