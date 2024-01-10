using HackerNews.Server.Models;
using HackerNewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Tests.ApiMockData
{
    public static class StoriesDetails
    {
        public static List<int> Stories = new List<int>() { 1 };
        //    new ResponseMessage()
        //{
        //    Message = string.Empty,
        //    Status = true,
        //    Result = new List<int>() { 1 }
        //};
        

        public static StoryDto StoriesDetailsDto { get; set; }

        public static StoryDto Get()
        {
            return new StoryDto()
            {
                Id = 1,
                Score = 1,
                By = "stevefan1999",
                Time = 1704439734,
                Title = "Microsoft Announces AppCAT: Simplifying Azure Migration for .NET Apps",
                Type = "story",
                Url = "https://www.infoq.com/news/2024/01/appcat-azure-dotnet/",
                Descendants = 0
            };
        }

        public static ResponseMessage GetStories()
        {
            return new ResponseMessage()
            {
                Status = true,
                Result = new List<StoryDto>(){ new StoryDto()
            {
                Id = 1,
                Score = 1,
                By = "stevefan1999",
                Time = 1704439734,
                Title = "Microsoft Announces AppCAT: Simplifying Azure Migration for .NET Apps",
                Type = "story",
                Url = "https://www.infoq.com/news/2024/01/appcat-azure-dotnet/",
                Descendants = 0
            }
            }
           
            };
        }
    }
}
