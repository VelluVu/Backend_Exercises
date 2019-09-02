using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
        string[] bikeData;

        public Task<int> GetBikeCountInStation ( string stationName )
        {
            int numVal = 0;
            bikeData = System.IO.File.ReadAllLines ( @"C:\Users\vellu\Desktop\Backend" );

            for ( int i = 0 ; i < bikeData.Length ; i++ )
            {

                int index = bikeData [ i ].IndexOf ( ":" );

                string subString;

                if ( index != -1 )
                {
                    subString = bikeData [ i ].Substring ( 0, index );

                    if ( subString == stationName )
                    {
                        subString = bikeData [ bikeData.Length - 1 ];

                        try
                        {
                            numVal = Int32.Parse ( subString );
                        }
                        catch ( FormatException e )
                        {
                            Console.WriteLine ( e.Message );
                        }
                    }
                }

            }

            return numVal;

        }
    }
}
