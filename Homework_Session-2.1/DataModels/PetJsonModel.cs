using Newtonsoft.Json;

namespace Homework_Session_2._1.DataModels
{
    /// <summary>
    /// User JSON Model
    /// </summary>
    public class PetJsonModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("category")]
        public Category3 Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUrls")]
        public List<string> PhotoUrls { get; set; }

        [JsonProperty("tags")]
        public List<Tag2> Tags { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class Category3
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Tag2
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
