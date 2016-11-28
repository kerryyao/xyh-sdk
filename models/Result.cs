using System.Collections.Generic;

namespace com.nbugs.xyh.models
{
    /// <summary>
    /// suguo.yao 2016-11-28
    /// 小虫返回的JSON结果总框架
    /// </summary>
    public class Result<T>
    {
        public int code { get; set; }
        public T r { get; set; }
        public List<T> rs { get; set; }
    }
}