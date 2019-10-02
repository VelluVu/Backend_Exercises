using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Items
{
    public class NewItem
    {
        [StringLength(128)]
        public string Name { get; set; }
    }
}
