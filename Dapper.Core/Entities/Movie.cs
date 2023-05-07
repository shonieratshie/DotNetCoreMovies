using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Core.Entities
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public Boolean Deleted { get; set; }
        public string Category { get; set; }

    }
}
