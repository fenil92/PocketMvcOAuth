using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketCoreMvc.Services
{
    public interface ITokenService
    {
         Task<string> GetToken();
         string GetConsumerKey();
    }
}
