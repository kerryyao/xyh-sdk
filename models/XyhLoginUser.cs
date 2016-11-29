using System.Collections.Generic;

namespace com.nbugs.xyh.models
{
    public class XyhLoginUser
    {
        public string passtype { get; set; }
        public string passid { get; set; }
        public string orgid { get; set; }
        public string orgname { get; set; }
        public string orgtype { get; set; }
        public string oauthtoken { get; set; }
        public List<Users> users { get; set; }
    }
}