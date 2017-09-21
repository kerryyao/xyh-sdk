namespace com.nbugs.xyh.models
{
    /// <summary>
    /// suguo.yao 2017-6-15
    /// 增加字段content、msg、videoUrl、audioUrl以支持短信及其它媒体
    /// </summary>
    public class XyhNewsMessage
    {
        public string title { get; set; }
        public string summary { get; set; }
        public string picUrl { get; set; }
        public string content { get; set; }
        public string msg { get; set; }
        public string videoUrl { get; set; }
        public string audioUrl { get; set; }
        public Link link { get; set; } = new Link();

        public class Link
        {
            public int linkId { get; set; }
            public string urlParams { get; set; }
        }
    }
}