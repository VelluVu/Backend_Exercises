using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Items
{
    public class ModifiedItem
    {
        [Range ( 1, 99 )]
        public int Level { get; set; }
    }
}
