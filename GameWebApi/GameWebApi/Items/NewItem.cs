using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Items
{
    public class NewItem
    {
        public string Name { get; set; }
        public ItemType type { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
