using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace Homework_Session_2._1
{
    [TestClass]
    public class HttpClientTests
    {
        private static HttpClient httpClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        private static readonly string UsersEndpoint = "pet";

        private static string GetURL(string enpoint) => $"{BaseURL}{enpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<Pet> cleanUpList = new List<Pet>();

        [TestInitialize]
        public void TestInitialize()
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var httpResponse = await httpClient.DeleteAsync(GetURL($"{UsersEndpoint}/{data.Id}"));
            }
        }

        [TestMethod]
        public async Task PutMethod()
        {
            #region create data

            // Create Json Object
            Pet pet = new Pet()
            {
                Id = 101,
                Category = new Category { Name = "Juan Catergory" },
                Name = "Juan",
                PhotoUrls = new string[] { "Juan Url" },
                Tags = new Category[] { new Category { Name = "Juan Tags" } },
                Status = "available"
            };

            // Serialize Content
            var request = JsonConvert.SerializeObject(pet);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            await httpClient.PostAsync(GetURL(UsersEndpoint), postRequest);

            #endregion

            #region get Name of the created data

            // Get Request
            var getResponse = await httpClient.GetAsync(GetURI($"{UsersEndpoint}/{pet.Id}"));

            // Deserialize Content
            var listUserData = JsonConvert.DeserializeObject<Pet>(getResponse.Content.ReadAsStringAsync().Result);

            // filter created data
            var createdUserData = listUserData.Name;

            #endregion

            #region send put request to update data

            // Update value of userData
            pet = new Pet()
            {
                Id = listUserData.Id,
                Category = listUserData.Category,
                Name = "Juan.put.updated",
                PhotoUrls = listUserData.PhotoUrls,
                Tags = listUserData.Tags,
                Status = listUserData.Status
            };

            // Serialize Content
            request = JsonConvert.SerializeObject(pet);
            postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Put Request
            var httpResponse = await httpClient.PutAsync(GetURL($"{UsersEndpoint}"), postRequest);

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            #endregion

            #region get updated data

            // Get Request
            getResponse = await httpClient.GetAsync(GetURI($"{UsersEndpoint}/{pet.Id}"));

            // Deserialize Content
            listUserData = JsonConvert.DeserializeObject<Pet>(getResponse.Content.ReadAsStringAsync().Result);

            // filter created data
            createdUserData = listUserData.Name;

            #endregion

            #region cleanup data

            // Add data to cleanup list
            cleanUpList.Add(listUserData);

            #endregion

            #region assertion

            // Assertion
            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 200");
            Assert.AreEqual(pet.Name, createdUserData, "Name not matching");

            #endregion

        }
    }
}
