using RestSharp;
using Homework_Session_2._1.DataModels;
using Homework_Session_2._1.Resources;
using Homework_Session_2._1.Tests.TestData;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Homework_Session_2._1.Helpers
{
    /// <summary>
    /// Demo class containing all methods for users
    /// </summary>
    public class UserHelper
    {
        /// <summary>
        /// Send POST request to add new user
        /// </summary>
        ///

        public static async Task<PetJsonModel> AddNewUser(RestClient client)
        {
            var newPetData = GeneratePets.demoPets();
            var postRequest = new RestRequest(Endpoints.PostUser());

            //Send Post Request to add new user
            postRequest.AddJsonBody(newPetData);
            var postResponse = await client.ExecutePostAsync<PetJsonModel>(postRequest);

            var createdPetData = newPetData;
            return createdPetData;
        }
    }
}
