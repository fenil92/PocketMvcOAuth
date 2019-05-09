using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PocketCoreMvc.Services;

namespace PocketCoreMvc.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IApiService apiService;
        public MessagesController(IApiService apiService)
        {
            this.apiService = apiService;
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody] HttpContext activity) {
            var result = activity;
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        public async Task<IList<string>> HandleRedirect() {
            //var values = await apiService.GetValues();
            return new List<string>();
        }
    }
}