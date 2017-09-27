using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scannex
{
    public class Locations
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public int? parent_id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

    }
}
