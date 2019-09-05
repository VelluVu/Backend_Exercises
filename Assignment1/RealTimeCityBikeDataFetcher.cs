using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assignment1
{
    //API http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental
    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        private readonly HttpClient _httpClient = new HttpClient ( );
        string URL = "http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental";
        int bikeCount = 0;
        bool found = false;

        public async Task<int> GetBikeCountInStation ( string stationName )
        {
            if ( stationName.Any ( char.IsDigit ) )
            {
                throw new ArgumentException ( "Given station name contains numbers" );
            }

            var data = await _httpClient.GetStringAsync ( URL );
            BikeRentalStationList bikeData = JsonConvert.DeserializeObject<BikeRentalStationList> ( data );

            for ( int i = 0 ; i < bikeData.stations.Count ; i++ )
            {
                if ( bikeData.stations [ i ].name == stationName )
                {
                    bikeCount = bikeData.stations [i].bikesAvailable;
                    found = true;
                }
            }

            if( found )
            {
                return bikeCount;
            }
            else
            {
                throw new NotFoundException ( "Not Found" );
            }
        }
    }
}
