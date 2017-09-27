using System.Collections.Generic;

namespace com.nbugs.xyh.models
{
    public class XyhMessage<T>
    {
        public string msgId { get; set; }
        public string funcId { get; set; }
        public string msgtype { get; set; }
        public string parentsReceive { get; set; }
        public string studentReceive { get; set; }
        public string orgId { get; set; }
        public string senderId { get; set; }
        public string userIds { get; set; }
        public string deptIds { get; set; }
        public string userRoles { get; set; }
        public string excludeUserIds { get; set; }
        public string withSubdepts { get; set; }
        public T msg { get; set; }
    }
}