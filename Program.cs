using System;
using System.Threading.Tasks;

// dotnet new console TO CREATE EMPTY CONSOLE PROJECT
//RUN WITH < dotnet run station_name > COMMAND

namespace Backend
{
    class Program
    {
        static async Task Main ( string [ ] args)
        {
            string mode = "";
            Console.WriteLine ( args [ 0 ] );

            Console.WriteLine ( "Type Station Name" );
            mode = Console.ReadLine ( );

            if (mode == "offline" || mode == "Offline")
            {   
                OfflineCityBikeDataFetcher fetcher = new OfflineCityBikeDataFetcher ( );
                var task = await fetcher.GetBikeCountInStation ( "Petikontie" );
                Console.WriteLine ( task );
            }
            else if( mode == "realtime" || mode == "Realtime" )
            {          
                RealTimeCityBikeDataFetcher fetcher = new RealTimeCityBikeDataFetcher ( );
                var task = await fetcher.GetBikeCountInStation ( "Petikontie" );
                Console.WriteLine ( task );
            }
        }
    }

    //JSON DEPENCY
    //dotnet add package NewtonSoft.Json
    //Use dotnet restore 

}