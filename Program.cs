using System;
using System.Threading.Tasks;

// dotnet new console TO CREATE EMPTY CONSOLE PROJECT
//RUN WITH < dotnet run station_name > COMMAND

namespace Backend
{
    class Program
    {
        static void Main ( string [ ] args )
        {
            MainAsync ( args ).GetAwaiter ( ).GetResult ( );
        }

        static async Task MainAsync ( string [ ] args )
        {

            string mode;
            string stationName;

            try
            {
                mode = args [ 0 ];
                stationName = args [ 1 ];
            }
            catch ( Exception ex )
            {
                Console.WriteLine ( "Not enough arguments! use 'offline|realtime station_name'" );
                return;
            }

            ICityBikeDataFetcher fetcher;

            if (mode == "offline" || mode == "Offline")
            {   
                fetcher = new OfflineCityBikeDataFetcher ( );
                var task = await fetcher.GetBikeCountInStation ( stationName );
                Console.WriteLine ( task );
            }
            else if( mode == "realtime" || mode == "Realtime" )
            {          
                fetcher = new RealTimeCityBikeDataFetcher ( );
                var task = await fetcher.GetBikeCountInStation ( stationName );
                Console.WriteLine ( task );
            }
            else
            {
                Console.WriteLine ( "Use offline or realtime" );
                
                return;
            }         
        }
    }

    //JSON DEPENCY
    //dotnet add package NewtonSoft.Json
    //Use dotnet restore 

}