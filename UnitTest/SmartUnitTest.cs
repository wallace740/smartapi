using DALCore.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using Wallace740_SmartApi.Models;

namespace UnitTest
{
    [TestClass]
    public class SmartUnitTest : BaseTest
    {
        [TestMethod]
        public void GetToken_Test()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"Wk7tBtWG6asCrE6rsovGsgRNj08RoRar\",\"client_secret\":\"kgNW0aC91j5WNEWktf9of8jTrfnOdfDQy5ARQJdTJ3X6DBXqqMj6qdvN6EpkcKLQ\",\"audience\":\"https://dev-wallace740.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetPrivateScoped_Test()
        {
            var accessTokenResponse = GetAccessToken();
            string tokenValue = string.Empty;
            if (accessTokenResponse != null)
            {
                var tokenOutput = JsonConvert.DeserializeObject<TokenModel>(accessTokenResponse.Content);
                tokenValue = tokenOutput.access_token;
            }

            var client = new RestClient("https://localhost:5001/api/private-scoped"); //admin permission

            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + tokenValue); //MGMT_API_ACCESS_TOKEN
            request.AddHeader("cache-control", "no-cache");
            //request.AddParameter("application/json", "{ \"scopes\": [ { \"value\": \"write:products\", \"description\": \"Admin role\" }, { \"value\": \"read:products\", \"description\": \"Consumer role\" } ] }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var x = response;
        }

        [TestMethod]
        public void InsertProduct_Test()
        {
            var accessTokenResponse = GetAccessToken();
            string tokenValue = string.Empty;
            if (accessTokenResponse != null)
            {
                var tokenOutput = JsonConvert.DeserializeObject<TokenModel>(accessTokenResponse.Content);
                tokenValue = tokenOutput.access_token;
            }

            var client = new RestClient("https://localhost:5001/api/insert");

            var prd = new Product() { Name = "Murat", Brand = "Tifozi" };
            string jsonBodyRaw = JsonConvert.SerializeObject(prd);

            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + tokenValue); //MGMT_API_ACCESS_TOKEN
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("application/json", jsonBodyRaw, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var re = response;
            Assert.IsNotNull(re);
        }

        private IRestResponse GetAccessToken()
        {
            RestRequest req;
            var client = BaseTest.PrepareRestClient(Method.POST, out req);
            IRestResponse response = client.Execute(req);
            return response;
        }
    }
}
