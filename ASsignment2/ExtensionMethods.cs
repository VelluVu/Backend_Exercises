using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ASsignment2
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> getMoreThanOnceRepeated<T> ( this IEnumerable<T> extList, Func<T, object> groupProps ) where T : class
        { //Return only the second and next reptition
            return extList
                .GroupBy ( groupProps )
                .SelectMany ( z => z.Skip ( 1 ) ); //Skip the first occur and return all the others that repeats
        }
        public static IEnumerable<T> getAllRepeated<T> ( this IEnumerable<T> extList, Func<T, object> groupProps ) where T : class
        {
            //Get All the lines that has repeating
            return extList
                .GroupBy ( groupProps )
                .Where ( z => z.Count ( ) > 1 ) //Filter only the distinct one
                .SelectMany ( z => z );//All in where has to be retuned
        }
    }
}
