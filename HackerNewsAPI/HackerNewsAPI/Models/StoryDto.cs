namespace HackerNews.Server.Models
{
    public class StoryDto
    {
        /// <summary>
        /// By of story
        /// </summary>
        public string By { get; set; }
        /// <summary>
        /// Descendants for story
        /// </summary>
        public int Descendants { get; set; }
        /// <summary>
        /// Id of Story
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Score of story
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Time of story
        /// </summary>
        public int Time { get; set; }
        /// <summary>
        /// Title of  story
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Type of story
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Url of story
        /// </summary>
        public string Url { get; set; }
    }
}
