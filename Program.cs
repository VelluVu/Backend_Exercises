using System;
using System.Net.Http;

// dotnet new console TO CREATE EMPTY CONSOLE PROJECT
namespace Backend {
    class Program {
        static void Main (string[] args) {
            //RUN WITH < dotnet run station_name > COMMAND
            Console.WriteLine (args[0]);     
        }
    }

    //JSON DEPENCY
    //dotnet add package NewtonSoft.Json
    //Use dotnet restore 

}