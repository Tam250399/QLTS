using System;
using System.Collections.Generic;

namespace GS.NewAPI.Infrastruture.Response
{
    public class BaseResponse<T>
    {
        public const int SUCCESS_CODE = 200;
        public const int ERROR_CODE = 500;
        public const int NOT_FOUND_CODE = 404;
        public const int ERROR = 503; // lỗi khác
        public const int SUCCESS_PARTIAL_CODE = 206;
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public T Data { get; set; }
        public BaseResponse()
        {
            Success = true;
        }
        public BaseResponse(bool success)
        {
            Success = success;
        }
        public BaseResponse(int _code, string _msg)
        {
            StatusCode = _code;
            Message = _msg;
            Date = DateTime.Now;
        }

        public static BaseResponse<T> CreateSuccessMessage(string _msg, dynamic _objectInfo = null)
        {
            var msgret = new BaseResponse<T>(SUCCESS_CODE, _msg);
            msgret.Data = _objectInfo;
            msgret.Date = DateTime.Now;
            msgret.Success = true;
            return msgret;
        }

        public static BaseResponse<T> CreateErrorMessage(string _msg, dynamic _objectInfo = null)
        {
            var msgret = new BaseResponse<T>(ERROR_CODE, _msg);
            msgret.Data = _objectInfo;
            msgret.Date = DateTime.Now;
            msgret.Success = false;
            return msgret;
        }
        public static BaseResponse<T> CreateNotFoundMessage(string _msg, dynamic _objectInfo = null)
        {
            var msgret = new BaseResponse<T>(NOT_FOUND_CODE, _msg);
            msgret.Data = _objectInfo;
            msgret.Date = DateTime.Now;
            msgret.Success = false;
            return msgret;
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
        public ListResponse(IList<T> data)
        {
            Results = data;
            Count = data.Count;
        }
        public ListResponse(IList<T> data, int count = 0)
        {
            Results = data;
            Count = count;
        }
    }
}
