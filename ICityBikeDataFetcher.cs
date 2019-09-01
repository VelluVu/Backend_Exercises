using System.Threading.Tasks;

namespace Backend
{
    public interface ICityBikeDataFetcher
    {
        Task<int> GetBikeCountInStation ( string stationName );
    }
}