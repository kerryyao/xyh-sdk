using Newtonsoft.Json;

namespace com.nbugs.xyh.open
{
    public static class XyhExtendHelper
    {
        /// <summary>
        /// suguo.yao 2016-11-30
        /// 通过用户token获取用户code，用于主从应用跳转
        /// </summary>
        public static string getCode(string usertoken)
        {
            string url = string.Format(@"{0}/oauth2/grant/code?oauth_token={1}", appConfig.url, usertoken);
            var content = XyhApiHelper.getHttpContent(url).Result;
            /*
            {
                "grant_code": "94fcaf66d8aa4262bda88e7324a73ff01480480792212",
                "expire": 300
            }
            */
            var result = JsonConvert.DeserializeObject<dynamic>(content);
            return result.grant_code ?? string.Empty;
        }
    }
}