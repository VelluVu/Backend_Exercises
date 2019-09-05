using System;

namespace Assignment1
{
    class NotFoundException : Exception
    {
 
        public NotFoundException ( string v )
        {

            Console.WriteLine ( v );

        }
    }
}
