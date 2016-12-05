using System.Collections.Generic;

namespace com.nbugs.xyh.models
{
    public class XyhMessage<T>
    {
        public string msgId { get; set; }
        public int funcId { get; set; }
        public bool parentsReceive { get; set; }
        public bool studentReceive { get; set; }
        public string orgId { get; set; }
        public string senderId { get; set; }
        public List<string> userIds { get; set; }
        public List<string> deptIds { get; set; }
        public List<string> userRoles { get; set; }
        public List<string> excludeUserIds { get; set; }
        public bool withSubdepts { get; set; }
        public T msg { get; set; }
    }
}