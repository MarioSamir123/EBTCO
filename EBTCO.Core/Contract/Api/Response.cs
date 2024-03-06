using System.Net;

namespace EBTCO.Core.Api
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class APIResponse<T> : BaseResponse<T>
    {
        public int StatusCode
        {
            get
            {
                return (int)HttpStatusCode;
            }
        }
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccessStatusCode
        {
            get
            {
                return StatusCode >= 200 && StatusCode <= 299;
            }
        }
    }
}
