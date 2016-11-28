namespace com.nbugs.xyh
{
    /// <summary>
    /// suguo.yao 2016-11-28
    /// SDK配置类
    /// </summary>
    public static class appConfig
    {
        public static string url { get; set; }
        public static string client_id { get; set; }
        public static string client_secret { get; set; }
        public static bool debug { get; set; }

        static appConfig()
        {
            url = @"https://open.xiaoyuanhao.com/cgi-bin";
        }
    }
}