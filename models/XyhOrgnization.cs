using System.Collections.Generic;

namespace com.nbugs.xyh.models
{
    /// <summary>
    /// suguo.yao 2016-11-30
    /// XyhOrgnization对象用来描述用户在组织机构内的角色信息
    /// </summary>
    public class XyhOrgnization
    {
        public string orgId { get; set; }
        public string deptId { get; set; }
        public string deptName { get; set; }
        public string deptName2 { get; set; }
        public string deptType { get; set; }
        public string deptType2 { get; set; }
        public string parentId { get; set; }
        public int deptSeq { get; set; }
        public List<XyhOrgnization> subDepts { get; set; }
        public List<XyhUser> users { get; set; }
    }
}