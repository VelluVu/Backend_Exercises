using System;

namespace Backend
{
    class NotFoundException : Exception
    {
 
        public NotFoundException ( string v )
        {

            Console.WriteLine ( v );

        }
    }
}
