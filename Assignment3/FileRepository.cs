using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

public class FileRepository : IRepository
{

    string path = @"C:\Users\Veli-Matti\Desktop\Backend\Assignment3\game-dev.txt";

    Task<Player> IRepository.Get ( Guid id )
    {
        throw new NotImplementedException ( );
    }

    Task<Player [ ]> IRepository.GetAll ( )
    {
        throw new NotImplementedException ( );
    }

    async Task<Player> IRepository.Create ( Player player )
    {
        string text =
            player.Id + "\n" +
            player.Name + "\n" + 
            player.Level + "\n" + 
            player.Score + "\n" + 
            player.IsBanned + "\n\n";
        

        await File.WriteAllTextAsync ( path, text );

        return player;
    }

    async Task<Player> IRepository.Modify ( Guid id, ModifiedPlayer player )
    {
        var text = await File.ReadAllLinesAsync ( path );

        Player _player = new Player ( ) ;


        for ( int i = 0 ; i < text.Length ; i++ )
        {
            if(text[i] == id.ToString())
            {
                _player.Id = id;
                _player.Name = text [ i + 1 ];
                _player.Level = int.Parse ( text [ i + 2 ]);
                _player.Score = int.Parse(text [ i + 3 ]);       
                _player.IsBanned = bool.Parse ( text [ i + 4 ] );

                return _player;
            }
        }

        throw new ArgumentException ( "Id cannot be found from the repository" );
    

    }

    Task<Player> IRepository.Delete ( Guid id )
    {
        throw new NotImplementedException ( );
    }
}