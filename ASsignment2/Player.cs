﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ASsignment2
{
    public class Player : IPlayer
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public List<Item> Items { get; set; }     
    }
}
