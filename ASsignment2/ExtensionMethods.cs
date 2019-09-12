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
            return player.Items [ player.Items.Max ( x => x.Level ) ];
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

        public static void CheckGuidDublicates ( this List<IPlayer> players )
        {
            
        }

    }

}
