using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    //API http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental
    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        private readonly HttpClient _httpClient = new HttpClient ( );
        string URL = "http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental";

        public Task<int> GetBikeCountInStation ( string stationName )
        {
            throw new NotImplementedException ( );
        }

        public async Task QueryURL ( )
        {
            var data = await _httpClient.GetStringAsync ( URL );
            BikeRentalStationList bikeData = JsonConvert.DeserializeObject<BikeRentalStationList> (data);

        }
    }
}
