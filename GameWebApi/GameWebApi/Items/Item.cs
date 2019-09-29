using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Items
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemType type { get; set; }
        public bool IsUsing { get; set; }
        public DateTime CreationTime { get; set; }
    }

    public enum ItemType
    {
        Sword,
        Potion,
        Shield,
    }
}
