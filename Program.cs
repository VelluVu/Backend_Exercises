using System;
using System.Net.Http;
using System.Threading.Tasks;

// dotnet new console TO CREATE EMPTY CONSOLE PROJECT
//RUN WITH < dotnet run station_name > COMMAND

namespace Backend
{
    class Program
    {
        static void/*async Task*/ Main ( string [ ] args )
        {
            Console.WriteLine ( args [ 0 ] );
            RealTimeCityBikeDataFetcher fetcher = new RealTimeCityBikeDataFetcher ( );
            var task = /*await */ fetcher.GetBikeCountInStation ( "Petikontie" );
            Console.WriteLine ( task );


        }
    }

    //JSON DEPENCY
    //dotnet add package NewtonSoft.Json
    //Use dotnet restore 

}