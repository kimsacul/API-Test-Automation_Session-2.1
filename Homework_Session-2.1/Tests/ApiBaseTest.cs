using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Homework_Session_2._1.DataModels;

namespace Homework_Session_2._1.Tests
{
    public class ApiBaseTest
    {
        public RestClient RestClient { get; set; }

        public PetJsonModel PetDetails { get; set; }

        [TestInitialize]
        public void Initilize()
        {
            RestClient = new RestClient();
        }

        [TestCleanup]
        public void CleanUp()
        {

        }

    }
}
