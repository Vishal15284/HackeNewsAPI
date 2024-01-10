namespace HackerNewsAPI.Models
{
    public class ResponseMessage
    {
        /// <summary>
        /// Show the status 
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Response message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Get list of stories
        /// </summary>
        public dynamic Result { get; set; }
    }
}
