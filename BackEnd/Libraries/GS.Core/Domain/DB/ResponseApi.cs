using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.DB
{
    public class ResponseApi
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public dynamic Data { get; set; }
        public ResponseApi() { }
        public ResponseApi(bool _Status, string _msg)
        {
            Status = _Status;
            Message = _msg;
            Date = DateTime.Now;
        }
        public ResponseApi(bool _Status, string _msg, dynamic _Data)
        {
            Status = _Status;
            Message = _msg;
            Data = _Data;
            Date = DateTime.Now;
        }
        public static ResponseApi CreateSuccessMessage(string _msg, dynamic _Data = null)
        {
            var msgret = new ResponseApi(true, _msg);
            msgret.Data = _Data;
            msgret.Date = DateTime.Now;
            msgret.Status = true;
            return msgret;
        }

        public static ResponseApi CreateErrorMessage(string _msg, dynamic _Data = null)
        {
            var msgret = new ResponseApi(false, _msg);
            msgret.Data = _Data;
            msgret.Date = DateTime.Now;
            msgret.Status = false;
            return msgret;
        }
    }
}
