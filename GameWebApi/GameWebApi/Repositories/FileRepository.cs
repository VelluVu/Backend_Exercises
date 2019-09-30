using GameWebApi.Items;
using GameWebApi.Players;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameWebApi.Repositories
{
    public class FileRepository : IRepository
    {
        static string path = Path.Combine ( AppDomain.CurrentDomain.BaseDirectory, "game-dev.txt" );

        public async Task<Player> Get ( Guid id )
        {

            string [ ] textArray = await File.ReadAllLinesAsync ( path );

            for ( int i = 0 ; i < textArray.Length ; i++ )
            {
                int index = textArray [ i ].IndexOf ( ":" );
                string subString;
                if ( index != -1 )
                {
                    subString = textArray [ i ].Substring ( index, textArray [ i ].Length );

                    if ( subString == id.ToString ( ) )
                    {
                        try
                        {
                            Player p = new Player ( );
                            p.Id = id;
                            p.Name = textArray [ i + 1 ].Substring ( textArray [ i ].IndexOf ( ":" ), textArray [ i + 1 ].Length );
                            p.Level = int.Parse ( textArray [ i + 2 ].Substring ( textArray [ i ].IndexOf ( ":" ), textArray [ i + 2 ].Length ) );
                            p.Score = int.Parse ( textArray [ i + 3 ].Substring ( textArray [ i ].IndexOf ( ":" ), textArray [ i + 3 ].Length ) );
                            p.IsBanned = bool.Parse ( textArray [ i + 4 ].Substring ( textArray [ i ].IndexOf ( ":" ), textArray [ i + 4 ].Length ) );

                            return p;
                        }
                        catch ( FormatException e )
                        {
                            Console.WriteLine ( e.Message );
                        }
                    }
                }
            }

            throw new ArgumentException ( "Id cannot be found from the repository" );

        }

        public async Task<Player [ ]> GetAll ( )
        {

            List<Player> playerList = new List<Player> ( );

            string [ ] textArray = await File.ReadAllLinesAsync ( path );

            for ( int i = 0 ; i < textArray.Length ; i++ )
            {
                int index = textArray [ i ].IndexOf ( ":" );
                string subString;
                if ( index != -1 )
                {
                    subString = textArray [ i ].Substring ( 0, index -1 );
                    Console.WriteLine ( subString ); 
                    if ( subString == "id" )
                    {
                        Player p = await Get ( new Guid ( textArray [ i ].Substring ( index, textArray [ i ].Length ) ) );

                        playerList.Add ( p );

                    }
                }
            }

            if(playerList.Count == 0)
            {
                throw new ArgumentException ( "List is empty" );
            }

            return playerList.ToArray ( );
        }

        public async Task<Player> Create ( Player player )
        {

            //Make json formatted string from player class
            string text = JsonConvert.SerializeObject ( player );
            //Writes new player to the txt file
            //await File.AppendAllTextAsync ( path, textLine );

            //add player to txt file in web api
            await File.AppendAllTextAsync ( path, text );

            //return player
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

            string text = JsonConvert.SerializeObject ( playerList );
            File.WriteAllText ( path, text );

            return await Get ( id );
        }

        //Removes player from textfile and rewrites it
        public async Task<Player> Delete ( Guid id )
        {
            Player [ ] players = await GetAll ( );
            List<Player> playerList = players.ToList ( );

            bool removeSuccess = false;

            if ( playerList.Count == 0 )
            {
                throw new ArgumentException ( "List is empty" );
            }

            for ( int p = 0 ; p < playerList.Count ; p++ )
            {
                if ( id == playerList [ p ].Id )
                {
                    playerList.Remove ( playerList [ p ] );
                    removeSuccess = true;
                }
            }

            if ( !removeSuccess )
            {
                throw new ArgumentException ( "Id cannot be found from the repository" );
            }


            string text = JsonConvert.SerializeObject ( playerList );
            File.WriteAllText ( path, text );

            return await Get ( id );
        }

        public Task<Item> GetItem ( Player player, Guid id )
        {
            throw new NotImplementedException ( );
        }

        public Task<Item [ ]> GetAllItems ( Player player )
        {
            throw new NotImplementedException ( );
        }

        public async Task<Item> CreateItem ( Player player, Item item )
        {

            player.itemList.Add ( item );

            return item;

        }

        public Task<Item> UseItem ( Player player, Guid id, ModifiedItem item )
        {
            throw new NotImplementedException ( );
        }

        public Task<Item> DeleteItem ( Player player, Guid id )
        {
            throw new NotImplementedException ( );
        }
    }
}
