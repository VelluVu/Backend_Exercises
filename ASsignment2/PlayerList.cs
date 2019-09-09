using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ASsignment2
{
    class PlayerList
    {
        public int playerAmount = 1000000;

        public List<Player> players = new List<Player> ( );

        public PlayerList ()
        {
            for ( int i = 0 ; i < playerAmount ; i++ )
            {
                players.Add ( new Player ( ) );
                players [ i ].Id = new Guid ( );
            }

            CheckGuidDublicates ( );
        }

        public void CheckGuidDublicates()
        {
            //Make dictionary pair with guids and players
            Dictionary<Guid, Player> pairs = new Dictionary<Guid, Player> ( );
            foreach ( var p in players )
            {
                pairs.Add ( p.Id, p  );
            }

            //Check the duplicates from dictionary
            var query = pairs.GroupBy ( x => x )
              .Where ( g => g.Count ( ) > 1 )
              .ToDictionary ( x => x.Key, y => y.Count ( ) );

        }

    }
}
