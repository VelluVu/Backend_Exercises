using System;
using System.Collections.Generic;
using System.Text;

namespace ASsignment2
{
    class PlayerForAnotherGame : IPlayer
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public List<Item> Items { get; set; }
    }
}
