using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ASsignment2
{
    public static class ExtensionMethods
    {

        public static Item GetHighestLevelItem ( this Player player )
        {
            Item item = player.Items [ player.Items.Max ( x => x.Level ) ];

            return item;
        }

        public static void InstantiatePlayers ( this List<IPlayer> players, int playerAmount )
        {
            for ( int i = 0 ; i < playerAmount ; i++ )
            {
                Player player = new Player ( );
                player.Id = new Guid ( );
                players.Add ( player );
              
            }
        }

        public static void CheckGuidDublicates ( )
        {


        }
     
    }

}
}
