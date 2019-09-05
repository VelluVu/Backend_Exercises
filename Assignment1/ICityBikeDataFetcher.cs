using System.Threading.Tasks;

namespace Assignment1
{
    public interface ICityBikeDataFetcher
    {
        Task<int> GetBikeCountInStation ( string stationName );
    }
}