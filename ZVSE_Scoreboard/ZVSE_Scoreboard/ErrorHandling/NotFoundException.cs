using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZVSE_Scoreboard.ErrorHandling
{
    public class NotFoundException : Exception
    {
        public NotFoundException ( string msg ) : base (msg) {}
    }
}
