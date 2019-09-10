using System;
using System.Collections.Generic;
using System.Text;

namespace ASsignment2
{
    public class Item
    {
        public Guid Id { get; set; }
        public int Level { get; set; }

        //Could be extension methods
        public Item [ ] GetItems ( Player player )
        {

            Item [ ] itemList = new Item [ player.Items.Count ];

            if ( itemList.Length == 0 )
            {
                return null;
            }

            for ( int i = 0 ; i < itemList.Length ; i++ )
            {
                if ( player.Items [ i ] != null )
                    itemList [ i ] = player.Items [ i ];
            }

            return itemList;
        }

        public static Item [ ] GetItemsWithLinq ( Player player )
        {

            Item [ ] itemList = player.Items.ToArray ( );

            if ( itemList.Length == 0 )
            {
                return null;
            }

            return itemList;
        }

        public Item FirstItem ( Player player )
        {

            if ( player.Items == null )
            {
                return null;
            }

            return player.Items [ 0 ];

        }

        public Item FirstItemWithLinq ( Player player )
        {
            if ( player.Items.Count == 0 )
            {
                return null;
            }

            return player.Items.First ( );
        }
    }

   
}
