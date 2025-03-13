using System;
using System.Xml.Serialization;

namespace GS.Core.Domain.Common
{
    public class MessageReturn
    {
        public const string SUCCESS_CODE = "00";
        public const string ERROR_CODE = "01";
        public const string NOT_FOUND_CODE = "04";
        public const string ERROR = "05"; // lỗi khác
        public const string SUCCESS_PARTIAL_CODE = "06";
        public MessageReturn() { }
        public MessageReturn(string _code, string _msg)
        {
            Code = _code;
            Message = _msg;
            Date = DateTime.Now;
        }
        public MessageReturn(string _code, string _msg, string _idRecord)
        {
            Code = _code;
            Message = _msg;
            IdRecord = _idRecord;
            Date = DateTime.Now;
        }
        public MessageReturn(string _code, string _msg, dynamic _objectInfo)
        {
            Code = _code;
            Message = _msg;
            ObjectInfo = _objectInfo;
            //Data = _objectInfo;
            Date = DateTime.Now;
        }
        public MessageReturn(string _code, string _msg, string _idRecord, dynamic _objectInfo)
        {
            Code = _code;
            Message = _msg;
            ObjectInfo = _objectInfo;
            //Data = _objectInfo;
            Date = DateTime.Now;
            IdRecord = _idRecord;
        }
        public string Code { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string IdRecord { get; set; }
        /// <summary>
        /// Co the luu struct 1 object, hoac 1 list object
        /// </summary>
        public dynamic ObjectInfo { get; set; }
        public DateTime Date { get; set; }
        //public dynamic Data { get; set; }
        public static MessageReturn CreateSuccessMessage(string _msg, dynamic _objectInfo = null)
        {
            var msgret = new MessageReturn(SUCCESS_CODE, _msg);
            msgret.ObjectInfo = _objectInfo;
            //msgret.Data = _objectInfo;
            msgret.Date = DateTime.Now;
            msgret.Status = true;
            return msgret;
        }
      
        public static MessageReturn CreateErrorMessage(string _msg, dynamic _objectInfo = null)
        {
            var msgret = new MessageReturn(ERROR_CODE, _msg);
            msgret.ObjectInfo = _objectInfo;
            // msgret.Data = _objectInfo;
            msgret.Date = DateTime.Now;
            msgret.Status = false;
            return msgret;
        }
        public static MessageReturn CreateNotFoundMessage(string _msg, dynamic _objectInfo = null)
        {
            var msgret = new MessageReturn(NOT_FOUND_CODE, _msg);
            msgret.ObjectInfo = _objectInfo;
            // msgret.Data = _objectInfo;
            msgret.Date = DateTime.Now;
            msgret.Status = false;
            return msgret;
        }
    }
    public class MessageReturnDMDC
    {
        public const int DEFAULT = 0;
        public MessageReturnDMDC() { }
        public MessageReturnDMDC(int _code, string _msg)
        {
            //ERRORNUMBER = _code;
            ERRORMESSAGE = _msg;
        }
        public string ERRORMESSAGE { get; set; }
        //public int ERRORNUMBER { get; set; }
        public static MessageReturnDMDC CreateSuccessMessage(string _msg)
        {
            var msgret = new MessageReturnDMDC(DEFAULT, _msg);
            return msgret;
        }

        public static MessageReturnDMDC CreateErrorMessage(string _msg)
        {
            var msgret = new MessageReturnDMDC(DEFAULT, _msg);
            return msgret;
        }
        public static MessageReturnDMDC CreateNotFoundMessage(string _msg)
        {
            var msgret = new MessageReturnDMDC(DEFAULT, _msg);
            return msgret;
        }
    }
}
