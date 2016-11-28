using System;
using Newtonsoft.Json;
using System.Net.Http;
using com.nbugs.xyh.models;
using System.Net.Http.Headers;

namespace com.nbugs.xyh.open
{
    /// <summary>
    /// suguo.yao 2016-11-28
    /// 该类以static方式提供了若干接口实例，开发者可直接调用
    /// 接口相关文档见 http://open.xiaoyuanhao.com
    /// </summary>
    public static class XyhApiHelper
    {
        static string token = string.Empty;
        static DateTime expire = DateTime.Now;

        /// <summary>
        /// 获取应用token,采用系统内配clientid
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="clientsecret"></param>
        /// <returns></returns>
        public static string getToken()
        {
            try
            {
                if (!string.IsNullOrEmpty(token) && DateTime.Now < expire)      //令牌有效，返回true
                    return token;

                string url = string.Format(@"{0}/oauth2/token?grant_type=client_credentials&client_id={1}&client_secret={2}", appConfig.url, appConfig.client_id, appConfig.client_secret);
                var httpclient = new HttpClient();
                var contentstring = httpclient.GetStringAsync(url).Result;
                var content = JsonConvert.DeserializeObject<dynamic>(contentstring);
                token = (string)(content["oauth_token"] ?? string.Empty);
                if (!string.IsNullOrEmpty(token))
                {
                    expire = DateTime.Now.AddSeconds((int)(content["expires_in"] ?? 0));
                }
            }
            catch (Exception ex)
            {
                if (appConfig.debug)
                    throw ex;
            }
            return token;
        }

        /// <summary>
        /// suguo.yao 2016-6-23
        /// 通过用户CODE获取用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Result<UserModel> loadbycode(string code)
        {
            string url_token = string.Format(@"{0}/loginuser/loadbycode?client_id={1}&client_secret={2}&code={3}", appConfig.url, appConfig.client_id, appConfig.client_secret, code);

            var httpclient = new HttpClient();
            var content = httpclient.GetStringAsync(url_token).Result;
            var JResult = JsonConvert.DeserializeObject<Result<UserModel>>(content);
            return JResult;
        }

        //用户登录后，根据access_token获取登录用户信息
        public static UserModel loadbytoken()
        {
            string url = string.Format(@"{0}/loginuser/loadbytoken?oauth_token={1}", appConfig.url, getToken());

            var httpclient = new HttpClient();
            var content = httpclient.GetStringAsync(url).Result;
            var jresult = JsonConvert.DeserializeObject<Result<UserModel>>(content);
            if (jresult.code == 0)
                return jresult.r;
            else
                return null;
        }

        ///<summary>
        /// suguo.yao 2016-11-28
        ///</summary>
        public static dynamic UserClassinfo()
        {
            string url = string.Format(@"{0}/ajaxLoaderSchoolOrgnization.do?method=ajaxJybJSON_LoadMyClass&oauth_token={1}&allClasses=true", appConfig.url, getToken());

            HttpClient httpclient = new HttpClient();
            var content = httpclient.GetStringAsync(url).Result;
            var jresult = JsonConvert.DeserializeObject<dynamic>(content);
            if (jresult.code == "0")
                return jresult.rs;
            else
                return null;
        }

        /// <summary>
        /// suguo.yao 2016-11-28
        /// 消息推送
        /// </sumarry>
        public static dynamic SubmitMessage(dynamic model, string token = "")
        {
            string url = string.Format(@"{0}/msg/pusher/batch?oauth_token={1}", appConfig.url, getToken());

            using (var httpclient = new HttpClient())
            {
                var str = JsonConvert.SerializeObject(model);
                HttpContent httpcontent = new StringContent(str);
                httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage resMessage = httpclient.PostAsync(url, httpcontent).Result;
                var ret = resMessage.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<dynamic>(ret);
            }
        }
    }
}