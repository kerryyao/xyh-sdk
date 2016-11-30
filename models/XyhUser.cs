using System.Collections.Generic;

namespace com.nbugs.xyh.models
{
    /// <summary>
    /// suguo.yao 2016-11-30
    /// XyhUser对象用来描述开放平台的用户基本信息
    /// </summary>
    public class XyhUser
    {
        public string orgId { get; set; }
        public List<XyhOrgUsers> userRoles { get; set; }
        public string userType { get; set; }
        public List<string> details { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public string userType2 { get; set; }
        public string sex { get; set; }
        public string userCode { get; set; }
        public string orgUniqId { get; set; }
        public string birthday { get; set; }
        public List<XyhUserContact> contacts { get; set; }
        public List<XyhUserCard> cards { get; set; }
    }
}