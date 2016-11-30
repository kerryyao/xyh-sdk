namespace com.nbugs.xyh.models
{
    /// <summary>
    /// suguo.yao 2016-11-30
    /// XyhUserCard对象用来描述用户的配卡信息，该卡信息可用于学校一卡通系统等
    /// </summary>
    public class XyhUserCard
    {
        public string orgId{get;set;}
        public string userId{get;set;}
        public string cardId{get;set;}
        public string remark{get;set;}
    }
}