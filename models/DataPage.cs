using System.Collections.Generic;

namespace com.nbugs.xyh.models
{
    public class DataPage<T>
    {
        public uint pageCount { get; set; }
        public uint pageIndex { get; set; }
        public uint pageRecordCount { get; set; }
        public uint pagesize { get; set; }
        public uint totalRecordCount { get; set; }
        public List<T> rs { get; set; }
    }
}