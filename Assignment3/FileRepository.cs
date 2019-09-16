using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

public class FileRepository : IRepository
{

    string path = @"C:\Users\Veli-Matti\Desktop\Backend\Assignment3\game-dev.txt";
    int playerCount = 0;

    async Task<Player> IRepository.Get ( Guid id )
    {

        var text = await File.ReadAllLinesAsync ( path );

        Player _player = new Player ( );

        for ( int i = 0 ; i < text.Length ; i++ )
        {
            if ( text [ i ] == id.ToString ( ) )
            {
                _player.Id = id;
                _player.Name = text [ i + 1 ];
                _player.Level = int.Parse ( text [ i + 2 ] );
                _player.Score = int.Parse ( text [ i + 3 ] );
                _player.IsBanned = bool.Parse ( text [ i + 4 ] );

                return _player;
            }
        }
    
        throw new ArgumentException ( "Id cannot be found from the repository" );
}

    async Task<Player [ ]> IRepository.GetAll ( )
    {

        Player [ ] players = new Player [ playerCount ];

        var text = await File.ReadAllLinesAsync ( path );

        for ( int i = 0 ; i < text.Length ; i++ )
        {
            if(text[i])
        }





        return players;

    }

    async Task<Player> IRepository.Create ( Player player )
    {
        string text =
            "id : " + player.Id + "\n" +
            "name : " + player.Name + "\n" + 
            "level : " +player.Level + "\n" + 
            "score : " + player.Score + "\n" + 
            "isbanned : " + player.IsBanned + "\n\n";

        //Writes new player to the txt file
        await File.AppendAllTextAsync ( path, text );

        playerCount++;

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
                text [ i + 3 ] = player.Score.ToString ( );
                _player.IsBanned = bool.Parse ( text [ i + 4 ] );

                return _player;
            }
        }

        throw new ArgumentException ( "Id cannot be found from the repository" );
    

    }

    Task<Player> IRepository.Delete ( Guid id )
    {
        throw new NotImplementedException ( );

        playerCount--;
    }
}