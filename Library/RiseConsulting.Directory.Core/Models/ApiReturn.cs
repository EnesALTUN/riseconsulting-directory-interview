namespace RiseConsulting.Directory.Core.Models
{
    public class ApiReturn<T> where T : class
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public T Data { get; set; } = null;
    }
}