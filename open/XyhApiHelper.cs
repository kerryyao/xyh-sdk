using System;
using Newtonsoft.Json;
using System.Net.Http;
using com.nbugs.xyh.models;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

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

        internal static async Task<string> getHttpContent(string uri)
        {
            var httpclient = new HttpClient();
            return await httpclient.GetStringAsync(uri);
        }

        //进行小虫二次urlencoding
        internal static string getUrlEncodeTypes(this List<string> types)
        {
            //进行URLENCODE转换
            string typestring = string.Empty;
            foreach (var t in types)
            {
                typestring += "," + System.Net.WebUtility.UrlEncode(System.Net.WebUtility.UrlEncode(t));
            }
            return typestring.TrimStart(',');
        }

        #region 获取登录对象的接口
        public static class loginuser
        {
            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取用户授权认证时重定向到开放平台的URL
            /// 在原协议上增加了funcid和urlid，以便为主从应用跳转生成url
            /// </summary>
            public static string genLoginUrl(string url, string protalid = "", string funcid = "", string urlid = "")
            {
                return string.Format(@"{0}/oauth2/user/authorize?client_id={1}&response_type=code&redirect_uri={2}{3}{4}{5}", appConfig.url, appConfig.client_id, System.Net.WebUtility.UrlEncode(url), string.IsNullOrEmpty(protalid) ? "" : "&portalid=" + protalid, string.IsNullOrEmpty(funcid) ? "" : "&funcid=" + funcid, string.IsNullOrEmpty(urlid) ? "" : "&urlid=" + urlid);
            }

            /// <summary>
            /// suguo.yao 2016-6-23
            /// 根据code临时码获取用户登录信息
            /// </summary>
            public static XyhLoginUser getLoginUserByCode(string code)
            {
                string url_token = string.Format(@"{0}/loginuser/loadbycode?client_id={1}&client_secret={2}&code={3}", appConfig.url, appConfig.client_id, appConfig.client_secret, code);

                var content = getHttpContent(url_token).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhLoginUser>>(content);
                if (JResult.code == 0)
                    return JResult.r;
                else
                    return null;
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

        static string getLoaderParam(this string url, XyhUserLoaderParam param)
        {
            if (param == null)
                return url;

            StringBuilder p = new StringBuilder();
            if (param.withUserRoleDetail)
                p.Append(@"&with_roledetail=true");
            if (param.withCard)
                p.Append(@"&with_card=true");
            if (param.withContacts)
                p.Append(@"&with_contacts=true");
            if (param.withOrgExtInfo)
                p.Append(@"&with_orgext=true");

            return url + p.ToString();
        }

        #region 获取用户信息的接口
        public static class user
        {
            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取单个用户基本信息
            /// </summary>
            public static XyhUser get(string orgid, string userid, XyhUserLoaderParam param = null)
            {
                string url = string.Format(@"{0}/user/get?orgid={1}&userid={2}&oauth_token={3}", appConfig.url, orgid, userid, getToken());

                var content = getHttpContent(url.getLoaderParam(param)).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhUser>>(content);
                if (JResult.code == 0)
                    return JResult.r;
                else
                    return null;
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取多个用户基本信息
            /// </summary>
            public static List<XyhUser> list(string orgid, string userids, XyhUserLoaderParam param = null)
            {
                string url = string.Format(@"{0}/user/list?orgid={1}&userids={2}&oauth_token={3}", appConfig.url, orgid, userids, getToken());

                var content = getHttpContent(url.getLoaderParam(param)).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhUser>>(content);
                if (JResult.code == 0)
                    return JResult.rs;
                else
                    return null;
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 列表形式获取机构内的用户信息,增加roles
            /// </summary>
            public static List<XyhUser> deptUsers(string orgid, List<string> deptids, string roles = null, XyhUserLoaderParam param = null)
            {
                string rolestring = string.Empty;
                if (!string.IsNullOrEmpty(roles))
                {
                    rolestring = @"&roles=" + System.Net.WebUtility.UrlEncode(System.Net.WebUtility.UrlEncode(roles));
                }
                string url = string.Format(@"{0}/orguser/list?orgid={1}&deptids={2}{3}&oauth_token={4}", appConfig.url, orgid, deptids.getUrlEncodeTypes(), rolestring, getToken());

                var content = getHttpContent(url.getLoaderParam(param)).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhUser>>(content);
                if (JResult.code == 0)
                    return JResult.rs;
                else
                    return null;
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 分页形式获取机构内的用户信息
            /// </summary>
            public static DataPage<XyhUser> deptUsers(int page, int pagesize, string orgid, List<string> deptids, XyhUserLoaderParam param = null)
            {
                string url = string.Format(@"{0}/orguser/page?orgid={1}&deptids={2}&oauth_token={3}&page={4}&pagesize={5}", appConfig.url, orgid, deptids.getUrlEncodeTypes(), getToken(), page, pagesize);

                var content = getHttpContent(url.getLoaderParam(param)).Result;
                var JResult = JsonConvert.DeserializeObject<Result<DataPage<XyhUser>>>(content);
                if (JResult.code == 0)
                    return JResult.r;
                else
                    return null;
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取部门及其部门内的人员信息
            /// </summary>
            public static List<XyhOrgnization> deptWithUsers(string orgid, List<string> deptids, XyhUserLoaderParam param = null)
            {
                string url = string.Format(@"{0}/orguser/map?orgid={1}&deptids={2}&oauth_token={3}", appConfig.url, orgid, deptids.getUrlEncodeTypes(), getToken());

                var content = getHttpContent(url.getLoaderParam(param)).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhOrgnization>>(content);
                if (JResult.code == 0)
                    return JResult.rs;
                else
                    return null;
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 分页形式获取机构下的全部用户信息
            /// </summary>
            public static DataPage<XyhUser> all(int page, int pagesize, string orgid, string usertype = "", XyhUserLoaderParam param = null)
            {
                string url = string.Format(@"{0}/orguser/all?orgid={1}&oauth_token={2}&page={3}&pagesize={4}", appConfig.url, orgid, getToken(), page, pagesize);
                if (!string.IsNullOrEmpty(usertype))
                    url += string.Format("&usertype={0}", usertype);
                var content = getHttpContent(url.getLoaderParam(param)).Result;
                var JResult = JsonConvert.DeserializeObject<Result<DataPage<XyhUser>>>(content);
                if (JResult.code == 0)
                    return JResult.r;
                else
                    return null;
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
                string url = string.Format(@"{0}/org/get?orgid={1}&deptid={2}&oauth_token={3}", appConfig.url, orgid, deptid, getToken());

                var content = getHttpContent(url).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhOrgnization>>(content);
                if (JResult.code == 0)
                    return JResult.r;
                else
                    return null;
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取某一组织机构内多个部门的基本信息
            /// </summary>
            public static List<XyhOrgnization> list(string orgid, List<string> deptids, string properties = null)
            {
                string url = string.Format(@"{0}/org/list?orgId={1}&deptids={2}&oauth_token={3}", appConfig.url, orgid, deptids.getUrlEncodeTypes(), getToken());

                var content = getHttpContent(url).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhOrgnization>>(content);
                if (JResult.code == 0)
                    return JResult.rs;
                else
                    return null;
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取下级部门信息，经测试只能获取下一级子部门
            /// </summary>
            public static List<XyhOrgnization> sublist(string orgid, string parentid, List<string> types, bool withSelf = false, string properties = null)
            {

                string url = string.Format(@"{0}/org/sublist?orgid={1}&parentid={2}&oauth_token={3}", appConfig.url, orgid, parentid, getToken());

                if (types != null && types.Count > 0)
                {
                    url += string.Format(@"&types={0}", types.getUrlEncodeTypes());
                }
                if (withSelf)
                {
                    url += "&withSelf=true";
                }
                var content = getHttpContent(url).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhOrgnization>>(content);
                if (JResult.code == 0)
                    return JResult.rs;
                else
                    return null;
            }

            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取组织机构的树形数据
            /// </summary>
            public static XyhOrgnization tree(string orgid, List<string> types, string properties = null)
            {
                string url = string.Format(@"{0}/org/tree?orgid={1}&oauth_token={2}", appConfig.url, orgid, getToken());

                if (types != null && types.Count > 0)
                {
                    url += string.Format(@"&types={0}", types.getUrlEncodeTypes());
                }
                var content = getHttpContent(url).Result;
                var JResult = JsonConvert.DeserializeObject<Result<XyhOrgnization>>(content);
                if (JResult.code == 0)
                    return JResult.r;
                else
                    return null;
            }
        }
        #endregion

        #region 获取机构定义的用户扩展字段信息的接口
        public static class orgext
        {
            /// <summary>
            /// suguo.yao 2016-11-29
            /// 获取某一组织机构内部门的基本信息
            /// <param name="usertype">教师、学生</param>
            /// </summary>
            public static List<string> get(string orgid, string usertype)
            {
                string url = string.Format(@"{0}/orgext/get?orgid={1}&usertype={2}&oauth_token={3}", appConfig.url, orgid, usertype, getToken());

                var content = getHttpContent(url).Result;
                var JResult = JsonConvert.DeserializeObject<Result<string>>(content);
                if (JResult.code == 0)
                    return JResult.rs;
                else
                    return null;
            }
        }
        #endregion

        #region 消息处理的接口
        public static class msg
        {
            /// <summary>
            /// suguo.yao 2016-11-28
            /// 文本消息推送
            /// </sumarry>
            public static bool batch(XyhMessage<XyhTextMessage> model)
            {
                string url = string.Format(@"{0}/msg/pusher/batch?oauth_token={1}", appConfig.url, getToken());

                using (var httpclient = new HttpClient())
                {
                    var str = JsonConvert.SerializeObject(model);
                    HttpContent httpcontent = new StringContent(str);
                    httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage resMessage = httpclient.PostAsync(url, httpcontent).Result;
                    var ret = resMessage.Content.ReadAsStringAsync().Result;
                    var r = JsonConvert.DeserializeObject<Result<string>>(ret);
                    if (r.code == 0)
                        return true;
                    else
                        return false;
                }
            }

            /// <summary>
            /// suguo.yao 2016-11-28
            /// 图文消息推送
            /// </sumarry>
            public static bool batch(XyhMessage<XyhNewsList> model)
            {
                string url = string.Format(@"{0}/msg/pusher/batch?oauth_token={1}", appConfig.url, getToken());

                using (var httpclient = new HttpClient())
                {
                    var str = JsonConvert.SerializeObject(model);
                    HttpContent httpcontent = new StringContent(str);
                    httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage resMessage = httpclient.PostAsync(url, httpcontent).Result;
                    var ret = resMessage.Content.ReadAsStringAsync().Result;
                    var r = JsonConvert.DeserializeObject<Result<string>>(ret);
                    if (r.code == 0)
                        return true;
                    else
                        return false;
                }
            }
        }
        #endregion
    }
}