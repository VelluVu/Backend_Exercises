using System;
using System.Threading.Tasks;

namespace Assignment1
{
    class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
        string[] bikeData;
        string path = @"C:\Users\vellu\Desktop\Backend";

        public async Task<int> GetBikeCountInStation ( string stationName )
        {
            int numVal = 0;
            bikeData = await System.IO.File.ReadAllLinesAsync ( path );

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

            throw new NotFoundException ( "Given station name was not found" );

        }
    }
}
