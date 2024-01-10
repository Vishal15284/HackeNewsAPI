using HackerNews.Server.Models;
using HackerNewsAPI.Models;
using Newtonsoft.Json;


namespace HackerNews.Server.Services
{
    public class HackerNewsServices : IHackerNews
    {
        private HttpClient _httpClient;
        public HackerNewsServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// to fetch the story by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StoryDto> GetStoryByID(int id)
        {
            StoryDto story = new StoryDto();
            try
            {
                var response = await _httpClient.GetAsync(string.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json?print=pretty", id));
                if (response.IsSuccessStatusCode)
                {
                    var storiesRes = response.Content.ReadAsStringAsync().Result;
                    story = JsonConvert.DeserializeObject<StoryDto>(storiesRes);
                }
            }
            catch (Exception ex)
            {

            }
            return story;
        }
        /// <summary>
        /// get all stories Ids
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseMessage> NewestStories()
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                List<int> stories = new List<int>();
                var response = await _httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty");
                if (response.IsSuccessStatusCode)
                {
                    var storiesRes = response.Content.ReadAsStringAsync().Result;
                    stories = JsonConvert.DeserializeObject<List<int>>(storiesRes);
                    responseMessage.Status = true;
                    responseMessage.Result = stories;
                }
                else
                {
                    responseMessage.Status = true;
                    responseMessage.Message = "Issue in hacker news side";
                    responseMessage.Result = stories;
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = false;
                responseMessage.Message = ex.Message;
            }

            return responseMessage;
        }
        /// <summary>
        /// get all stories from hacker news api
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseMessage> GetNewestStories()
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                var newestStories = await NewestStories();
                if (newestStories.Status)
                {
                    List<StoryDto> stories = new List<StoryDto>();
                    var task = ((List<int>)(newestStories.Result)).Select(GetStoryByID);
                    stories = (await Task.WhenAll(task)).ToList();
                    //remove those stories which title or URL have null value
                    stories = stories.Where(x => x.Title != null && x.Url != null && x.Url != "").ToList();
                    if (stories.Count > 0)
                    {
                        responseMessage.Status = true;
                        responseMessage.Result = stories;
                    }
                        else
                    {
                        responseMessage.Status = false;
                        responseMessage.Message = "Not get any hacker news story details";
                    }
                }
                else
                {
                    responseMessage = newestStories;
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = false;
                responseMessage.Message = ex.Message;
            }


            return responseMessage;
        }
    }
}
