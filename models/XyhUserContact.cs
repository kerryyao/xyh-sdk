namespace com.nbugs.xyh.models
{
    /// <summary>
    /// suguo.yao 2016-11-30
    /// XyhUserContact对象用来描述用户用户联系方式，一般作为XyhUser对象的属性存在
    /// </summary>
    public class XyhUserContact
    {
        public string type{get;set;}
        public string orgId{get;set;}
        public string userId{get;set;}
        public string  detail{get;set;}
        public string relation{get;set;}
        public string relationDetail{get;set;}
        public int seq{get;set;}
    }
}