namespace com.nbugs.xyh.models
{
    /// <summary>
    /// suguo.yao 2016-11-30
    /// XyhOrgUsers对象用来描述用户在组织机构内的角色信息
    /// </summary>
    public class XyhOrgUsers
    {
        public string orgId{get;set;}
        public string userId{get;set;}
        public string deptId{get;set;}
        public string userRole{get;set;}
        public System.DateTime inDate{get;set;}
        public string userRoleRemark{get;set;}
        public XyhOrgnization dept{get;set;}
        public XyhUser user{get;set;}
    }
}