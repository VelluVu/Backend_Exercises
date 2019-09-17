using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Assignment3.Players;

namespace Assignment3.Repositories
{

    public class FileRepository : IRepository
    {

        string path = @"C:\Users\Veli-Matti\Desktop\Backend\Assignment3\game-dev.txt";
        int playerCount = 0;

        public async Task<Player> Get ( Guid id )
        {

            var text = await File.ReadAllLinesAsync ( path );

            Player _player = new Player ( );

            for ( int i = 0 ; i < text.Length ; i++ )
            {

                int index = text [ i ].IndexOf ( ":" );
                string subString;

                if ( index != -1 )
                {
                    subString = text [ i ].Substring ( index + 1, text[i].Length );

                    if ( subString == id.ToString ( ) )
                    {
                        _player.Id = id;
                        _player.Name = text [ i ].Substring ( 0, index + 1 );
                        _player.Level = int.Parse ( text [ i ].Substring ( 0, index + 2 ) );
                        _player.Score = int.Parse ( text [ i ].Substring ( 0, index + 3 ) );
                        _player.IsBanned = bool.Parse ( text [ i ].Substring ( 0, index + 4 ) );

                        return _player;
                    }
                }
            }

            throw new ArgumentException ( "Id cannot be found from the repository" );
        }

        public async Task<Player [ ]> GetAll ( )
        {

            Player [ ] players = new Player [ playerCount ];
            int counter = 0;

            var text = await File.ReadAllLinesAsync ( path );

            for ( int i = 0 ; i < text.Length ; i++ )
            {
                int index = text [ i ].IndexOf ( ":" );
                string subString;

                if ( index != -1 )
                {
                    subString = text [ i ].Substring ( 0, 2 );

                    if ( subString == "id" )
                    {
                        string guid = text [ i ].Substring ( index + 1, text [ i ].Length );
                        Guid g = new Guid ( guid );
                        players [ counter ] = await Get ( g );
                        counter++;
                    }
                }
            }

            return players;
        }

        public async Task<Player> Create ( Player player )
        {
            string text =
                "id : " + player.Id + "\n" +
                "name : " + player.Name + "\n" +
                "level : " + player.Level + "\n" +
                "score : " + player.Score + "\n" +
                "isbanned : " + player.IsBanned + "\n\n";

            //Writes new player to the txt file
            await File.AppendAllTextAsync ( path, text );

            playerCount++;

            return player;
        }

        public async Task<Player> Modify ( Guid id, ModifiedPlayer player )
        {
            var text = await File.ReadAllLinesAsync ( path );

            Player _player = new Player ( );
            _player = await Get ( id );
            
            return _player;
        }

        public async Task<Player> Delete ( Guid id )
        {
            Player player = new Player ( );
            player = await Get ( id );

            playerCount--;

            return player;
        }
    }
}