using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework_Session_2._1.Helpers;
using Homework_Session_2._1.Resources;
using Homework_Session_2._1.DataModels;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Homework_Session_2._1.Tests
{
    [TestClass]
    public class PatternTests : ApiBaseTest
    {
        private static List<PetJsonModel> petCleanUpList = new List<PetJsonModel>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            PetDetails = await UserHelper.AddNewUser(RestClient);
        }

        [TestMethod]
        public async Task DemoGetPet()
        {
            //Arrange
            var demoGetRequest = new RestRequest(Endpoints.GetPetById(PetDetails.Id));
            petCleanUpList.Add(PetDetails);

            //Act
            var demoResponse = await RestClient.ExecuteGetAsync<PetJsonModel>(demoGetRequest);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, demoResponse.StatusCode, "Failed due to wrong status code.");
            Assert.AreEqual(PetDetails.Name, demoResponse.Data.Name);
            Assert.AreEqual(PetDetails.PhotoUrls[0], demoResponse.Data.PhotoUrls[0]);
            Assert.AreEqual(PetDetails.Tags[0].Name, demoResponse.Data.Tags[0].Name);
            Assert.AreEqual(PetDetails.Tags[0].Id, demoResponse.Data.Tags[0].Id);
            Assert.AreEqual(PetDetails.Status, demoResponse.Data.Status);
        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var data in petCleanUpList)
            {
                var deletePetRequest = new RestRequest(Endpoints.GetPetById(data.Id));
                var deletePetResponse = await RestClient.DeleteAsync(deletePetRequest);
            }
        }
    }
}
