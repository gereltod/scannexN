using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Scannex
{
    public class Signature
    {
        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }
        [JsonProperty("acl")]
        public string Acl { get; set; }
        [JsonProperty("success_action_status")]
        public string SuccessAction { get; set; }
        [JsonProperty("policy")]
        public string Policy { get; set; }
        [JsonProperty("X-amz-credential")]
        public string XamzCredential { get; set; }
        [JsonProperty("X-amz-algorithm")]
        public string XamzAlgorithm { get; set; }
        [JsonProperty("X-amz-date")]
        public string XamzDate { get; set; }
        [JsonProperty("X-amz-signature")]
        public string XamzSignature { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
