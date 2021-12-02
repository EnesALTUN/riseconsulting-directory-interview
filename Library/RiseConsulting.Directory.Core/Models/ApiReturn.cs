using Microsoft.AspNetCore.Http;

namespace RiseConsulting.Directory.Core.Models
{
    public class ApiReturn<T> where T : class
    {
        public bool Success { get; set; } = true;
        public int Code { get; set; } = StatusCodes.Status200OK;
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public T Data { get; set; } = null;
    }
}