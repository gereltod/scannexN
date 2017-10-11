using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scannex
{
    public class PostResponse
    {
        public string Location { get; set; }
        public string Bucket { get; set; }
        public string Key { get; set; }
        public string ETag { get; set; }
    }
}
