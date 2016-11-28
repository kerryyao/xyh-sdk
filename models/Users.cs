using System.Collections.Generic;

namespace com.nbugs.xyh.models
{
    public class Users
    {
        public List<Role> roles { get; set; }
        public string usertype { get; set; }
        public string relationdetail { get; set; }
        public string username { get; set; }
        public string userid { get; set; }
        public string relation { get; set; }
    }
}