using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scannex
{
    public class DocTypes
    {        
        public string name { get; set; }
        public int is_sensitive { get; set; }
        public string hashid { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<SubTypes> fields { get; set; }
    }
}
