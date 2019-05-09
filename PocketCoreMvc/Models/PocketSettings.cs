using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketCoreMvc.Models
{
    public class PocketSettings
    {
        public string TokenUrl { get; set; }
        //public string ClientId { get; set; }
        //public string ClientSecret { get; set; }
        public string consumer_key { get; set; }
        public string redirect_uri { get; set; }
    }
}
