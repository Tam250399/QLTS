//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DB
{
    public enum enumTrangThaiQueueProcessDB
    {
        DANG_CHO = 0,
        DA_GUI_REQUEST = 1,
        GUI_REQUEST_THANH_CONG = 2,
        GUI_REQUEST_THAT_BAI = 3
    }
    public enum enumLevelQueueProcessDB
    {
        LOW = 1,
        MEDIUM = 2,
        HIGH = 3,
        HIGHEST = 4

    }
    public partial class DB_QueueProcess : BaseEntity
    {
        public Guid? GUID { get; set; }
        public String GUID_RESPONSE { get; set; }
        public String API_RESPONSE { get; set; }
        public String URL_CALL { get; set; }
        public String DATA_INPUT { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public DateTime? LAST_SEND_REQUEST { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal CAP_DO_ID { get; set; }
        public Decimal? DOI_TUONG_ID { get; set; }

    }
    public class WebApi_Response
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }
    }

    public class DB_BAOCAO
    {
        public DB_BAOCAO() { }
        public string MA_BAO_CAO { get; set; }
        public string MA_BAO_CAO_KHO { get; set; }
        public string DATA_JSON { get; set; }
    }
}



