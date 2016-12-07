namespace com.nbugs.xyh.models
{
    public class Xyh_Result<T>
    {
        public string page { get; set; }
        public T data { get; set; }
        public int code { get; set; }
        public string msg { get; set; }
    }
}