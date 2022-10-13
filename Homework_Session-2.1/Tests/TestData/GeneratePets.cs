using Homework_Session_2._1.DataModels;
using System;

namespace Homework_Session_2._1.Tests.TestData
{
    public class GeneratePets
    {
        public static PetJsonModel demoPets()
        {
            List<Tag2> tags = new List<Tag2>();
            tags.Add(new Tag2()
            {
                Id = 1,
                Name = "Tag_1"
            });

            return new PetJsonModel
            {
                Id = 1,
                Category = new Category3()
                {
                    Id = 1,
                    Name = "Cat_1"
                },
                Name = "PetName_1",
                PhotoUrls = new List<string>() { "PhotoUrl_1" },
                Tags = tags,
                Status = "Available"
            };
        }
    }
}
