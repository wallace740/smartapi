using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace UnitTest
{
    public class BaseTest
    {
        /// <summary>
        /// BaseUrl = AuthDomain
        /// </summary>
        public const string BaseUrl = "https://dev-wallace740.us.auth0.com/oauth/token";

        public const string ClientId = "SZIN3ZzxNImz21VhPXY2IeeHkSvuwOEH";
        public const string ClientSecret = "ZJoCu9Gcl08O0yhuaIiKkdwJ3Wq22jiJLtANZ-oExGt81nZjHshRPIOthT61aRvO";
        public const string Audience = "https://localhost:5001/";
        public const string GrantType = "client_credentials";

        public BaseTest()
        {
        }

        public static RestClient PrepareRestClient(RestSharp.Method httpMethod, out RestRequest restRequest)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(httpMethod);//Method.POST
            request.AddHeader("content-type", "application/json");
            //request.AddParameter("application/json", "{\"client_id\":\"Wk7tBtWG6asCrE6rsovGsgRNj08RoRar\",\"client_secret\":\"kgNW0aC91j5WNEWktf9of8jTrfnOdfDQy5ARQJdTJ3X6DBXqqMj6qdvN6EpkcKLQ\",\"audience\":\"https://dev-wallace740.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            //request.AddParameter("application/json", "{\"client_id\":\"SZIN3ZzxNImz21VhPXY2IeeHkSvuwOEH\"," +
            //    "\"client_secret\":\"ZJoCu9Gcl08O0yhuaIiKkdwJ3Wq22jiJLtANZ-oExGt81nZjHshRPIOthT61aRvO\"," +
            //    "\"audience\":\"https://localhost:5001/\"," +
            //    "\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);

            request.AddParameter("application/json", BuildJsonToken(), ParameterType.RequestBody);

            restRequest = request;
            return client;
        }

        private static string BuildJsonToken()
        {
            var tokenInput = new TokenInputModel
            {
                client_id = ClientId,
                client_secret = ClientSecret,
                audience = Audience,
                grant_type = GrantType
            };
            string json_data = JsonConvert.SerializeObject(tokenInput);
            return json_data;
        }
    }

}
