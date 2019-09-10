using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ASsignment2
{
    public class Game<T> where T : IPlayer
    {
        private List<T> _players;

        public Game ( List<T> players )
        {
            _players = players;
        }

        public T [ ] GetTop10Players ( )
        {
            // ... write code that returns 10 players with highest scores

            T[] players = ( from p in _players
                            orderby p.Score descending
                            select p ).Take ( 10 ).ToArray();

            return players;
        }
    }
}
