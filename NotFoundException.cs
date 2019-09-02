using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    class NotFoundException : Exception
    {
        
        string message= "Not Found";

        public NotFoundException (  )
        {

            Console.WriteLine ( message );

        }
    }
}
