using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PocketCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PocketCoreMvc.Services
{
    public class PocketTokenService : ITokenService
    {
        private PocketToken token = new PocketToken();
        private readonly IOptions<PocketSettings> pocketSettings;

        public PocketTokenService(IOptions<PocketSettings> pocketSettings)
        {
            this.pocketSettings = pocketSettings;
        }

        public async Task<string> GetToken()
        {
            if (this.token == null || string.IsNullOrEmpty(this.token.AccessToken))
            {
                this.token = await this.GetNewAccessToken();
            }
            return token.AccessToken;
        }

        public string GetConsumerKey() => this.pocketSettings.Value.consumer_key;

        private async Task<PocketToken> GetNewAccessToken()
        {
            var token = new PocketToken();
            var client = new HttpClient();
            var client_id = this.pocketSettings.Value.consumer_key;
            var client_secret = this.pocketSettings.Value.redirect_uri;
            var clientCreds = System.Text.Encoding.UTF8.GetBytes($"{client_id}:{client_secret}");
            //client.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(clientCreds));
            // string yourJson =   $""{ "consumer_key"} "" {client_id},{"redirect_uri"}: {"pocketapp1234:authorizationFinished"}";

            var postMessage = new Dictionary<string, string>();
            postMessage.Add("consumer_key", client_id);
            postMessage.Add("redirect_uri", client_secret);
            var content = new StringContent(JsonConvert.SerializeObject(postMessage), Encoding.UTF8, "application/json");
            content.Headers.Add("X-Accept", "application/json");
            //var request = new HttpRequestMessage(HttpMethod.Post, this.pocketSettings.Value.TokenUrl)
            //{
            //   // Content = new FormUrlEncodedContent(postMessage)
            //   Content = content
            //};

            var response = await client.PostAsync(this.pocketSettings.Value.TokenUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<PocketToken>(json);
                //token.ExpiresAt = DateTime.UtcNow.AddSeconds(this.token.ExpiresIn);
            }
            else
            {
                throw new ApplicationException("Unable to retrieve access token from Pocket");
            }

            return token;
        }

        private class PocketToken
        {
            /// <summary>
            /// authorization code
            /// </summary>
            [JsonProperty(PropertyName = "code")]
            public string AccessToken { get; set; }

            [JsonProperty(PropertyName = "state")]
            public string State { get; set; }

            //public DateTime ExpiresAt { get; set; }

            //public string Scope { get; set; }

            //[JsonProperty(PropertyName = "token_type")]
            //public string TokenType { get; set; }

            //public bool IsValidAndNotExpiring
            //{
            //    get
            //    {
            //        return !String.IsNullOrEmpty(this.AccessToken) && this.ExpiresAt > DateTime.UtcNow.AddSeconds(30);
            //    }
            //}
            //}
        }
    }
}
