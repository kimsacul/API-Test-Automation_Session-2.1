using Homework_Session_2._1.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Homework_Session_2._1.Tests
{
    [TestClass]
    public class RestSharpTests
    {
        private static RestClient restClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        private static readonly string UserEndpoint = "pet";

        private static string GetURL(string enpoint) => $"{BaseURL}{enpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            restClient = new RestClient();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanUpList)
            {
                var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{data.Id}"));
                var restResponse = await restClient.DeleteAsync(restRequest);
            }
        }

        [TestMethod]
        public async Task PostMethod()
        {
            #region CreateUser
            //Create User
            List<Tag> tags = new List<Tag>();
            tags.Add(new Tag()
            {
                Id = 101,
                Name = "Tag_1"
            });

            PetModel pet = new PetModel()
            {
                Id = 101,
                Category = new Category2()
                {
                    Id = 101,
                    Name = "Cat_1"
                },
                Name = "PetName_1",
                PhotoUrls = new List<string>() { "PhotoUrl_1" },
                Tags = tags,
                Status = "Available"
            };

            // Send Post Request
            var temp = GetURI(UserEndpoint);
            var postRestRequest = new RestRequest(GetURI(UserEndpoint)).AddJsonBody(pet);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status code is not equal to 200");
            #endregion

            #region GetUser
            var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{pet.Id}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<PetModel>(restRequest);
            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(pet.Id, restResponse.Data.Id, "Id did not match.");
            Assert.AreEqual(pet.Category.Id, restResponse.Data.Category.Id, "Category Id did not match.");
            Assert.AreEqual(pet.Category.Name, restResponse.Data.Category.Name, "Category Name did not match.");
            Assert.AreEqual(pet.Name, restResponse.Data.Name, "Name did not match.");
            Assert.AreEqual(pet.PhotoUrls[0], restResponse.Data.PhotoUrls[0], "PhotoUrls did not match.");
            Assert.AreEqual(pet.Tags[0].Id, restResponse.Data.Tags[0].Id, "Tags Id did not match.");
            Assert.AreEqual(pet.Tags[0].Name, restResponse.Data.Tags[0].Name, "Tags Name did not match.");
            Assert.AreEqual(pet.Status, restResponse.Data.Status, "Status did not match.");
            #endregion

            #region CleanUp
            cleanUpList.Add(pet);
            #endregion
        }
    }
}
