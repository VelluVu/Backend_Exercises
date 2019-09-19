using GameWebApi.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Repositories
{
    public class FileRepository : IRepository
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + @"Repositories\game-dev.txt";

        public async Task<Player> Get ( Guid id )
        {

            string [ ] textArray = await File.ReadAllLinesAsync ( path );
            List<string> textList = textArray.ToList ( );

            foreach ( var line in textList )
            {
                string [ ] entries = line.Split ( ',' );

                if ( entries [ 0 ] == id.ToString ( ) )
                {
                    Player p = new Player ( );
                    p.Id = new Guid ( entries [ 0 ] );
                    p.Name = entries [ 1 ];
                    p.Level = int.Parse ( entries [ 2 ] );
                    p.Score = int.Parse ( entries [ 3 ] );
                    p.IsBanned = bool.Parse ( entries [ 4 ] );

                    return p;
                }

            }

            throw new ArgumentException ( "Id cannot be found from the repository" );
        }

        public async Task<Player [ ]> GetAll ( )
        {

            List<Player> playerList = new List<Player> ( );

            string [ ] textArray = await File.ReadAllLinesAsync ( path );
            List<string> textList = textArray.ToList ( );

            foreach ( var line in textList )
            {
                string [ ] entries = line.Split ( ',' );

                Player p = await Get ( new Guid ( entries [ 0 ] ) );

                playerList.Add ( p );
            }


            Player [ ] playerArray = playerList.ToArray ( );

            return playerArray;
        }

        public async Task<Player> Create ( Player player )
        {

            string textLine =
                player.Id + "," +
                player.Name + "," +
                player.Level + "," +
                player.Score + "," +
                player.IsBanned + ",";

            //Writes new player to the txt file
            await File.AppendAllTextAsync ( path, textLine );

            using ( StreamWriter writer = new StreamWriter ( path, true ) ) //// true to append data to the file
            {
                writer.WriteLine ( textLine );
            }

            return player;

        }

        //Modifies player and rewrites the text file
        public async Task<Player> Modify ( Guid id, ModifiedPlayer player )
        {

            Player [ ] players = await GetAll ( );
            List<Player> playerList = players.ToList ( );

            foreach ( var p in playerList )
            {
                if ( id == p.Id )
                {
                    p.Score = player.Score;

                }
            }

            string [ ] textLines = new string [ playerList.Count ];

            for ( int i = 0 ; i < textLines.Length ; i++ )
            {
                textLines [ i ] = playerList [ i ].Id.ToString ( ) + "," + playerList [ i ].Name + "," + playerList [ i ].Level.ToString ( ) + "," + playerList [ i ].Score.ToString ( ) + "," + playerList [ i ].IsBanned.ToString ( );
            }

            await File.WriteAllLinesAsync ( path, textLines );

            return await Get ( id );
        }

        //Removes player from textfile and rewrites it
        public async Task<Player> Delete ( Guid id )
        {
            Player [ ] players = await GetAll ( );
            List<Player> playerList = players.ToList ( );

            foreach ( var p in playerList )
            {
                if ( id == p.Id )
                {
                    playerList.Remove ( p );

                }
            }

            string [ ] textLines = new string [ playerList.Count ];

            for ( int i = 0 ; i < textLines.Length ; i++ )
            {
                textLines [ i ] = playerList [ i ].Id.ToString ( ) + "," + playerList [ i ].Name + "," + playerList [ i ].Level.ToString ( ) + "," + playerList [ i ].Score.ToString ( ) + "," + playerList [ i ].IsBanned.ToString ( );
            }

            await File.WriteAllLinesAsync ( path, textLines );

            return await Get ( id );
        }
    }
}
