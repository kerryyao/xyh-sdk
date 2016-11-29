using System;
using Newtonsoft.Json;
using System.Net.Http;
using com.nbugs.xyh.models;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                var contentstring = getHttpContent(url).Result;
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

        static async Task<string> getHttpContent(string uri)
        {
            var httpclient = new HttpClient();
            return await httpclient.GetStringAsync(uri);
        }

        #region 获取登录对象的接口
        public static class loginuser
        {
            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取用户授权认证时重定向到开放平台的URL
            /// </summary>
            public static string genLoginUrl(string url, string protalid = "")
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// suguo.yao 2016-6-23
            /// 根据code临时码获取用户登录信息
            /// </summary>
            public static Result<XyhLoginUser> getLoginUserByCode(string code)
            {
                string url_token = string.Format(@"{0}/loginuser/loadbycode?client_id={1}&client_secret={2}&code={3}", appConfig.url, appConfig.client_id, appConfig.client_secret, code);

                var content = getHttpContent(url_token).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhLoginUser>>(content);
                return JResult;
            }

            /// <summary>
            /// suguo.yao 2016-6-23
            /// 根据用户oauth_token获取用户登录信息
            /// </summary>
            public static XyhLoginUser getLoginUserByToken()
            {
                string url = string.Format(@"{0}/loginuser/loadbytoken?oauth_token={1}", appConfig.url, getToken());

                var content = getHttpContent(url).Result;
                var jresult = JsonConvert.DeserializeObject<Result<XyhLoginUser>>(content);
                if (jresult.code == 0)
                    return jresult.r;
                else
                    return null;
            }
        }
        #endregion

        #region 获取用户信息的接口
        public static class user
        {
            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取单个用户基本信息
            /// </summary>
            public static XyhUser get()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取多个用户基本信息
            /// </summary>
            public static List<XyhUser> list()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 列表形式获取机构内的用户信息
            /// </summary>
            public static List<XyhUser> deptUsers(string orgid, List<string> deptids, XyhUserLoaderParam param = null)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 分页形式获取机构内的用户信息
            /// </summary>
            public static DataPage<XyhUser> deptUsers(int page, int pagesize, string orgid, string usertype = "", XyhUserLoaderParam param = null)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 	获取部门及其部门内的人员信息
            /// </summary>
            public static List<XyhUser> deptWithUsers(string orgid, List<string> deptids, XyhUserLoaderParam param = null)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 分页形式获取机构下的全部用户信息
            /// </summary>
            public static DataPage<XyhUser> all(int page, int pagesize, string orgid, string usertype = "", XyhUserLoaderParam param = null)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region 获取机构部门信息的接口
        public static class org
        {
            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取某一组织机构内部门的基本信息
            /// </summary>
            public static XyhOrgnization get(string orgid, string deptid, string properties = null)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取某一组织机构内多个部门的基本信息
            /// </summary>
            public static List<XyhOrgnization> list(string orgid, List<string> deptids, string properties = null)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取下级部门信息
            /// </summary>
            public static List<XyhOrgnization> sublist(string orgid, string parentid, List<string> types, bool withSelf, string properties = null)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取组织机构的树形数据
            /// </summary>
            public static XyhOrgnization tree(string orgid, List<string> types, string properties = null)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region 获取机构定义的用户扩展字段信息的接口
        public static class orgext
        {
            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取某一组织机构内部门的基本信息
            /// </summary>
            public static string get(string orgid, string usertype)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region 消息处理的接口
        public static class msg
        {
            /// <summary>
            /// suguo.yao 2016-11-28
            /// 消息推送
            /// </sumarry>
            public static bool batch(dynamic model)
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
        #endregion
    }
}