using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PocketCoreMvc.Services
{
    public class SimpleApiService : IApiService
    {
        
        private readonly ITokenService tokenService;
        public SimpleApiService(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public async Task<IList<string>> GetValues()
        {
            List<string> values = new List<string>();
            
            var token = await tokenService.GetToken();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var postMessage = new Dictionary<string, string>();
            postMessage.Add("consumer_key", tokenService.GetConsumerKey());
            postMessage.Add("code", token);
            
            var content = new StringContent(JsonConvert.SerializeObject(postMessage), Encoding.UTF8, "application/json");
            content.Headers.Add("X-Accept", "application/json");
            using (var client = new HttpClient()) {
                var res = await client.PostAsync("https://getpocket.com/v3/oauth/authorize", content);
                if (res.IsSuccessStatusCode)
                {
                    var json = res.Content.ReadAsStringAsync().Result;
                    values = JsonConvert.DeserializeObject<List<string>>(json);
                }
                else
                {
                    values = new List<string> { res.StatusCode.ToString(), res.ReasonPhrase };
                }
            }
             
            return values;
        }

        public async Task<string> AuthenticateRequest() {
            var token =await tokenService.GetToken();
            string redirectUrl = "http://localhost:51741/api/messages";
            string url = $"https://getpocket.com/auth/authorize?request_token=" +token +"&redirect_uri="+ redirectUrl;
            return url;
        }
    }
}
