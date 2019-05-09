using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketCoreMvc.Services
{
    public interface IApiService
    {
        Task<IList<string>> GetValues();
        Task<string> AuthenticateRequest();
    }
}
