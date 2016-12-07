using System.Net.Http;
using System.Threading.Tasks;
using com.nbugs.xyh.models;
using Newtonsoft.Json;

namespace com.nbugs.xyh.open
{
    /// <summary>
    /// suguo.yao 2016-12-7
    /// 校园号PC端用户认证，目前仅适用PC端，服务由小虫技术二队维护
    /// 这里的token指用户级别token
    /// </summary>
    public static class XiaoYuanHaoHelper
    {
        static async Task<string> getHttpContent(string uri)
        {
            var httpclient = new HttpClient();
            return await httpclient.GetStringAsync(uri);
        }

        public static Xyh_Result<string> getToken(string code)
        {
            string uri = string.Format("https://auth.xiaoyuanhao.com/xyhauth/getTokenByCode?code={0}", code);
            string content = getHttpContent(uri).Result;
            return JsonConvert.DeserializeObject<Xyh_Result<string>>(content);
        }

        public static Xyh_Result<XyhLoginUser> getUser(string token, string schoolid = "", string mobile = "")
        {
            string uri = string.Format(" https://auth.xiaoyuanhao.com/xyhauth/getXyhUserByToken?token={0}&schoolId={1}&mobile={2}", token, schoolid, mobile);
            string content = getHttpContent(uri).Result;
            return JsonConvert.DeserializeObject<Xyh_Result<XyhLoginUser>>(content);
        }

    }
}