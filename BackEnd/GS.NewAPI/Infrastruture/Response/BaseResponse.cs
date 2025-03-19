using System.Collections.Generic;

namespace GS.NewAPI.Infrastruture.Response
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }   
        public int StatusCode { get; set; } 
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
        public BaseResponse()
        {
            Success = true;
        }
        public BaseResponse(bool success)
        {
            Success = success;
        }
    } 

    public partial class ListResponse<T>
    {
        public IList<T> Results { get; set; }

        public int Count { get; set; }

        public ListResponse()
        {
            Results = new List<T> { };
            Count = 0;
        }
        public ListResponse(IList<T> data, int count = 0)
        {
            Results = data;
            Count = count;
        }
    }
}
