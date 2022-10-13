using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_Session_2._1.Resources
{
    /// <summary>
    /// Class containing all endpoints used in API tests
    /// </summary>
    public class Endpoints
    {
        //Base URL
        public const string baseURL = "https://petstore.swagger.io/v2";

        public static string GetPetById (int id) => $"{baseURL}/pet/{id}";

        public static string PostUser() => $"{baseURL}/pet";

        public static string DeleteUserByUsername(int id) => $"{baseURL}/pet/{id}";
    }
}
