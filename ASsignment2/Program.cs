using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ASsignment2
{
    class Program
    {

        static void Main ( string [ ] args )
        {

            List<IPlayer> players = new List<IPlayer> ( );
            List<IPlayer> PlayerForAnotherGames = new List<IPlayer> ( );
                 
            Game<IPlayer> game1 = new Game<IPlayer> ( players );
            Game<IPlayer> game2 = new Game<IPlayer> ( PlayerForAnotherGames );
            var array1 = game1.GetTop10Players ( );
            var array2 = game2.GetTop10Players ( );

            for ( int i = 0 ; i < array1.Length ; i++ )
            {
                Console.WriteLine ( array1 [ i ] );
            }
            for ( int i = 0 ; i < array2.Length ; i++ )
            {
                Console.WriteLine ( array2 [ i ] );
            }


            ProcessEachItem ( (Player)players [ 0 ], PrintMessage );
            ProcessEachItem ( (Player)players [ 0 ], x => Console.WriteLine ( x.Id + " " + x.Level ) );
        }

        public static void PrintMessage ( Item item )
        {
            Console.WriteLine ( item.Id + ", " + item.Level );
        }

        public static void ProcessEachItem ( Player player, Action<Item> process )
        {
            if ( player.Items == null )
            {

            }
            else
            {
                foreach ( var item in player.Items )
                {
                    process ( item );
                }
            }
        }
    }
}