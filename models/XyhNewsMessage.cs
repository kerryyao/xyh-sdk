namespace com.nbugs.xyh.models
{
    public class XyhNewsMessage
    {
        public string title { get; set; }
        public string summary { get; set; }
        public string picUrl { get; set; }
        public class link
        {
            public int linkId { get; set; }
            public string urlParams { get; set; }
        }
    }
}