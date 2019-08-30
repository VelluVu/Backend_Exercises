using System;
using System.Net.Http;

// dotnet new console TO CREATE EMPTY CONSOLE PROJECT
namespace Backend {
    class Program {
        static void Main (string[] args) {
            //RUN WITH < dotnet run station_name > COMMAND
            Console.WriteLine (args[0]);

            RealTimeCityBikeDataFetcher _dataFetcher = new RealTimeCityBikeDataFetcher();
            _dataFetcher.QueryURL();
        }
    }

    public interface ICityBikeDataFetcher {
        Task<int> GetBikeCountInStation (string stationName);
    }

    //JSON DEPENCY
    //dotnet add package NewtonSoft.Json
    //Use dotnet restore 


    //API http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental
    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher 
    {
        private readonly HttpClient _httpClient = new HttpClient();
        //string http = "http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental";

        public async Task QueryURL() {
            var stringData = await _httpClient.GetStringAsync(URL);
        }
    }

    public class BikeRentalStationList {
        
    }
}